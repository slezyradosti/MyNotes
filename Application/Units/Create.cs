using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class Create
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
                var Unit = new Unit();
                _mapper.Map(request.Unit, Unit);
                
                var result = await _unitRepository.AddAsync(Unit) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to create Unit");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
