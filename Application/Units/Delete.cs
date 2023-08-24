using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Units
{
    public class Delete
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IUnitRepository _unitRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(IUnitRepository unitRepository, IUserAccessor userAccessor)
            {
                _unitRepository = unitRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var unit = await _unitRepository.GetOneAsync(request.Id);

                if (unit == null) return null;

                if (!await _unitRepository.IfUserHasAccessToTheUnit(request.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var result = await _unitRepository.RemoveAsync(unit) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete unit");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
