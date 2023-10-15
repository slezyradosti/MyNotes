using Application.Core;
using Domain.Models;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Photos
{
    public class List
    {
        public record Query(Guid noteId) : IRequest<Result<List<Photo>>>;

        public class Handler : IRequestHandler<Query, Result<List<Photo>>>
        {
            private readonly IPhotoRepository _photoRepository;
            private readonly IUserAccessor _userAccessor;
            private readonly INoteRepository _noteRepository;

            public Handler(IPhotoRepository photoRepository, IUserAccessor userAccessor, 
                INoteRepository noteRepository)
            {
                _photoRepository = photoRepository;
                _userAccessor = userAccessor;
                _noteRepository = noteRepository;
            }

            public async Task<Result<List<Photo>>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (!await _noteRepository.IfUserHasAccessToTheNote(request.noteId, _userAccessor.GetUserId()))
                {
                    return Result<List<Photo>>.Failure("You have no access to this data");
                }

                var photos = await _photoRepository.GetAllFromSpecificNoteAsync(request.noteId);

                return Result<List<Photo>>.Success(photos);
            }
        }

    }
}
