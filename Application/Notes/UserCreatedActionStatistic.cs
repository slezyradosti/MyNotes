using Application.Core;
using Domain.Repositories.Repos.DTOs;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class UserCreatedActionStatistic
    {
        public record Query() : IRequest<Result<List<GraphStatistic>>>;

        public class Handler : IRequestHandler<Query, Result<List<GraphStatistic>>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(INoteRepository noteRepository, IUserAccessor userAccessor)
            {
                _noteRepository = noteRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<List<GraphStatistic>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var statistic = await _noteRepository.GetUserCreatedNotesCount(_userAccessor.GetUserId());

                return Result<List<GraphStatistic>>.Success(statistic);
            }
        }
    }
}
