using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class Details
    {
        public class Query : IRequest<Result<Unit>>
        {
            public Guid UnitId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Unit>>
        {
            private readonly IUnitRepository _unitRepository;

            public Handler(IUnitRepository unitRepository)
            {
                _unitRepository = unitRepository;
            }

            public async Task<Result<Unit>> Handle(Query request, CancellationToken cancellationToken)
            {
                var unit = await _unitRepository.GetOneWithItsPagesAsync(request.UnitId);

                return Result<Unit>.Success(unit);
            }
        }
    }
}
