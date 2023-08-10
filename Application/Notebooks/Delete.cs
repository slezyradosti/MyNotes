using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Delete
    {
        public class Command : IRequest<MediatR.Unit>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, MediatR.Unit>
        {
            private readonly INotebookRepository _notebookRepository;

            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var notebook = await _notebookRepository.GetOneAsync(request.Id);
                await _notebookRepository.RemoveAsync(notebook);

                return MediatR.Unit.Value;
            }
        }
    }
}
