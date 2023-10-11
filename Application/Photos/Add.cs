using Application.Core;
using Domain.Models;
using Domain.Repositories.Photos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Photos
{
    public class Add
    {
        public record Command(Guid NoteId, IFormFile File) : IRequest<Result<Photo>>;

        public class Handler : IRequestHandler<Command, Result<Photo>>
        {
            private readonly INoteRepository _noteRepository;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IPhotoRepository _photoRepository;
            private readonly IUserAccessor _userAccessor;

            public Handler(INoteRepository noteRepository, IPhotoAccessor photoAccessor, 
                IPhotoRepository photoRepository, IUserAccessor userAccessor)
            {
                _noteRepository = noteRepository;
                _photoAccessor = photoAccessor;
                _photoRepository = photoRepository;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Photo>> Handle(Command request, CancellationToken cancellationToken)
            {
                var note = await _noteRepository.GetOneAsync(request.NoteId);

                if (note == null) return null;

                if (!await _noteRepository.IfUserHasAccessToTheNote(request.NoteId, _userAccessor.GetUserId()))
                {
                    return Result<Photo>.Failure("You have no access to this data");
                }

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId,
                    NoteId = note.Id,
                };

                var result = await _photoRepository.AddAsync(photo) > 0;

                if (!result) return Result<Photo>.Failure("Failed to add Photo");

                return Result<Photo>.Success(photo);
            }
        }
    }
}
