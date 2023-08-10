namespace Domain.Repositories.Repos.Interfaces
{
    public interface IRepository<T>
    {
        Task<int> AddAsync(T entity);
        Task<int> AddRangeAsync(IList<T> entities);
        Task<int> SaveAsync(T entity);
        Task<int> DeleteAsync(Guid id, byte[] rowVersion);
        Task<int> DeleteAsync(T entity);
        Task<T> GetOneAsync(Guid? id);
        Task<List<T>> GetAllAsync();

        Task<List<T>> ExecuteQueryAsync(string sqlQuery);
        Task<List<T>> ExecuteQueryAsync(string sqlQuery, object[] sqlParametersObjects);
    }
}
