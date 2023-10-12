using Application.Core;
using Domain.Repositories.Photos;
using Domain.Repositories.Repos;
using Domain.Repositories.Repos.Interfaces;
using IndentityLogic.Interfaces;
using MediatR;

namespace Application.Photos
{
    public class Delete
    {
        public record Command(string Id) : IRequest<Result<MediatR.Unit>>;
        public class Handler : IRequestHandler<Command, Result<MediatR.Unit>>
        {
            private readonly IPhotoRepository _photoRepository;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;

            public Handler(IPhotoRepository photoRepository, IPhotoAccessor photoAccessor,
                IUserAccessor userAccessor)
            {
                _photoRepository = photoRepository;
                _photoAccessor = photoAccessor;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var photo = await _photoRepository.GetOneAsync(request.Id);

                if (photo == null) return null;

                if (!await _photoRepository.IfUserHasAcessToThePhoto(request.Id, _userAccessor.GetUserId()))
                {
                    return Result<MediatR.Unit>.Failure("You have no access to this data");
                }

                var result = await _photoAccessor.DeletePhoto(photo.Id);

                if (result == null) return Result<MediatR.Unit>.Failure("Problem deleting photo from Cloud");

                var success = await _photoRepository.RemoveAsync(photo) > 0;

                if (!success) return Result<MediatR.Unit>.Failure("Problem deleting photo from database");

                return Result<MediatR.Unit>.Success(MediatR.Unit.Value);
            }
        }
    }
}
