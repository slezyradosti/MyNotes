using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class Edit
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public UnitDto Unit { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IUnitRepository _unitRepository;
            private readonly IMapper _mapper;

            public Handler(IUnitRepository unitRepository, IMapper mapper)
            {
                _unitRepository = unitRepository;
                _mapper = mapper;
            }
            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var unit = await _unitRepository.GetOneAsync(request.Unit.Id);

                if (unit == null) return null;

                _mapper.Map(request.Unit, unit);

                var result = await _unitRepository.SaveAsync(unit) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to edit unit");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
