using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Create
    {
        public class Command : IRequest<MediatR.Unit>
        {
            public Notebook Notebook { get; set; }
        }

        public class Handler : IRequestHandler<Command, MediatR.Unit>
        {
            private readonly INotebookRepository _notebookRepository;

            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<MediatR.Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _notebookRepository.AddAsync(request.Notebook);

                return MediatR.Unit.Value;
            }
        }
    }
}
