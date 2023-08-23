using Application.Core;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class Delete
    {
        public class Command : IRequest<Result<MediatR.Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly INotebookRepository _notebookRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(INotebookRepository notebookRepository, IUserAccessor userAccessor)
            {
                _notebookRepository = notebookRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<MediatR.Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var notebook = await _notebookRepository.GetOneAsync(request.Id);

                if (notebook == null) return null;

                if (!await _notebookRepository.IfUserHasAccessToTheNotebook(request.Id,
                    _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var result = await _notebookRepository.RemoveAsync(notebook) > 0;

                if (!result) return Result<MediatR.Unit>.Failure("Failed to delete the notebook");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
