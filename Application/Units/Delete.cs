using Application.Core;
using Domain.Repositories.Repos.Interfaces;
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

            public Handler(IUnitRepository unitRepository)
            {
                _unitRepository = unitRepository;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var unit = await _unitRepository.GetOneAsync(request.Id);

                if (unit == null) return null;

                var result = await _unitRepository.RemoveAsync(unit) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete unit");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
