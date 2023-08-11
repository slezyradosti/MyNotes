using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Details
    {
        public class Query : IRequest<Result<Notebook>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Notebook>>
        {
            private readonly INotebookRepository _notebookRepository;
            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<Result<Notebook>> Handle(Query request, CancellationToken cancellationToken)
            {
                var notebook = await _notebookRepository.DetailsAsync(request.Id);

                return Result<Notebook>.Success(notebook);
            }
        }
    }
}
