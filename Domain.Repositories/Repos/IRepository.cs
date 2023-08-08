namespace Domain.Repositories.Repos
{
    public interface IRepository<T>
    {
        int Add(T entity);
        int AddRange(IList<T> entities);
        int Save(T entity);
        int Delete(Guid id, byte[] rowVersion);
        int Delete(T entity);
        T GetOne(Guid? id);
        List<T> GetAll();

        List<T> ExecuteQuery(string sqlQuery);
        List<T> ExecuteQuery(string sqlQuery, object[] sqlParametersObjects);
    }
}
