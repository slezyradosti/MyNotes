using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
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
            private readonly IUserAccessor _userAccessor;

            public Handler(INotebookRepository notebookRepository, IUserAccessor userAccessor)
            {
                _notebookRepository = notebookRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Notebook>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _notebookRepository.IfUserHasAccessToTheNotebook(request.Id, 
                    _userAccessor.GetUserId()))
                {
                    return Result<Notebook>.Failure("You have no access to this data");
                }

                var notebook = await _notebookRepository.DetailsAsync(request.Id);

                return Result<Notebook>.Success(notebook);
            }
        }
    }
}
