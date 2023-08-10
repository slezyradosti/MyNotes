using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class List
    {
        public class Query : IRequest<List<Notebook>> 
        { 
            
        }

        public class Handler : IRequestHandler<Query, List<Notebook>>
        {
            private readonly INotebookRepository _notebookRepository;

            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<List<Notebook>> Handle(Query request, CancellationToken cancellationToken)
            {
                var notebooks = await _notebookRepository.GetAllAsync();

                if (notebooks == null) throw new Exception("There is no notebooks");

                return notebooks;
            }
        }
    }
}
