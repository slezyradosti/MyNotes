using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Notes
{
    public class Details
    {
        public record Query(Guid Id) : IRequest<Result<Note>>;

        public class Handler : IRequestHandler<Query, Result<Note>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(INoteRepository noteRepository, IUserAccessor userAccessor)
            {
                _noteRepository = noteRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Note>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _noteRepository.IfUserHasAccessToTheNote(request.Id, _userAccessor.GetUserId()))
                {
                    return Result<Note>.Failure("You have no access to this data");
                }

                var note = await _noteRepository.GetOneAsync(request.Id);

                return Result<Note>.Success(note);
            }
        }
    }
}
