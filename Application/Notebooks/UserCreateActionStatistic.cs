using Application.Core;
using Domain.Repositories.Repos.DTOs;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notebooks
{
    public class UserCreateActionStatistic
    {
        public record Query() : IRequest<Result<List<GraphStatistic>>>;

        public class Handler : IRequestHandler<Query, Result<List<GraphStatistic>>>
        {
            private readonly INotebookRepository _notebookRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(INotebookRepository notebookRepository, IUserAccessor userAccessor)
            {
                _notebookRepository = notebookRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<List<GraphStatistic>>> Handle(Query request, 
                CancellationToken cancellationToken)
            {
                var statistic = await _notebookRepository.GetUserCreatedNotebooksCount(_userAccessor.GetUserId());

                return Result<List<GraphStatistic>>.Success(statistic);
            }
        }
    }
}
