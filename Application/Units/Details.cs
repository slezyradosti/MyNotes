using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class Details
    {
        public record Query(Guid UnitId) : IRequest<Result<Unit>>;

        public class Handler : IRequestHandler<Query, Result<Unit>>
        {
            private readonly IUnitRepository _unitRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitRepository unitRepository, IUserAccessor userAccessor)
            {
                _unitRepository = unitRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _unitRepository.IfUserHasAccessToTheUnit(request.UnitId, _userAccessor.GetUserId()))
                {
                    return Result<Unit>.Failure("You have no access to this data");
                }

                var unit = await _unitRepository.GetOneWithItsPagesAsync(request.UnitId);

                return Result<Unit>.Success(unit);
            }
        }
    }
}
