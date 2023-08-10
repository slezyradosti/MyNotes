using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Details
    {
        public class Query : IRequest<Notebook>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Notebook>
        {
            private readonly INotebookRepository _notebookRepository;
            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<Notebook> Handle(Query request, CancellationToken cancellationToken)
            {
                var notebook = _notebookRepository.Details(request.Id);

                if (notebook == null) throw new Exception("There is notebook with this Id");

                return notebook;
            }
        }
    }
}
