using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Create
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public Notebook Notebook { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INotebookRepository _notebookRepository;

            public Handler(INotebookRepository notebookRepository)
            {
                _notebookRepository = notebookRepository;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _notebookRepository.AddAsync(request.Notebook) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to create notebook");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
