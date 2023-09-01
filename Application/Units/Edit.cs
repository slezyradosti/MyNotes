using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class Edit
    {
        public record Command(UnitDto Unit) : IRequest<Result<MediatR.Unit>>;

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IUnitRepository _unitRepository;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitRepository unitRepository, IMapper mapper, 
                IUserAccessor userAccessor)
            {
                _unitRepository = unitRepository;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }
            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var unit = await _unitRepository.GetOneAsync(request.Unit.Id);

                if (unit == null) return null;

                if (!await _unitRepository.IfUserHasAccessToTheUnit(request.Unit.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                _mapper.Map(request.Unit, unit);

                var result = await _unitRepository.SaveAsync(unit) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Faild to edit unit");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
