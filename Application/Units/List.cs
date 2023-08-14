using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using MediatR;
using Unit = Domain.Models.Unit;

namespace Application.Units
{
    public class List
    {
        public class Query : IRequest<Result<List<Unit>>>
        {
            public Guid NotebookId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Unit>>>
        {
            private readonly IUnitRepository _unitRepository;

            public Handler(IUnitRepository unitRepository)
            {
                _unitRepository = unitRepository;
            }
            public async Task<Result<List<Unit>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Unit>>.Success(await _unitRepository.GetAllFromNotebookAsync(request.NotebookId));
            }
        }
    }
}
