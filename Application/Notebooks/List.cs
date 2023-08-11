using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class List
    {
        public class Query : IRequest<Result<List<Notebook>>> 
        { 
            
        }

        public class Handler : IRequestHandler<Query, Result<List<Notebook>>>
        {
            private readonly INotebookRepository _notebookRepository;

            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<Result<List<Notebook>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Notebook>>.Success(await _notebookRepository.GetAllAsync());
            }
        }
    }
}
