using Microsoft.AspNetCore.Http;

namespace Domain.Repositories.Photos
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto (string photoId);
    }
}
