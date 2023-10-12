using Domain.Models;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IPhotoRepository
    {
        public Task<int> AddAsync(Photo photo);
        public Task<int> AddRangeAsync(IList<Photo> photos);
        public Task<int> DeleteAsStateAsync(string id, byte[] rowVersion);
        public Task<int> DeleteAsStateAsync(Photo photo);
        public Task<List<Photo>> ExecuteQueryAsync(string sqlQuery);
        public Task<List<Photo>> ExecuteQueryAsync(string sqlQuery, object[] sqlParametersObjects);
        public Task<List<Photo>> GetAllAsync();
        public Task<int> GetCountAsync();
        public Task<Photo> GetOneAsync(string? id);
        public Task<int> RemoveAsync(Photo photo);
        public Task<int> SaveAsync(Photo photo);
        public Task<bool> IfUserHasAcessToThePhoto(string id, Guid userId);
    }
}
