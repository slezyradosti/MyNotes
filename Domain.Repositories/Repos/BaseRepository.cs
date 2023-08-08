using Domain.Models.Base;
using Domain.Repositories.EFInitial;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Data.Entity.Infrastructure;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace Domain.Repositories.Repos
{
    public class BaseRepository<T> : IDisposable, IRepository<T> where T : BaseEntity, new()
    {

        private readonly DbSet<T> _table;
        private readonly DataContext _db;
        protected DataContext Context => _db;

        public BaseRepository()
        {
            _db = new DataContext();
            _table = _db.Set<T>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return SaveChanges();
        }

        public int AddRange(IList<T> entities)
        {
            _table.AddRange(entities);
            return SaveChanges();
        }

        public int Save(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public int Delete(Guid id, byte[] rowVersion)
        {
            _db.Entry(new T() { Id = id, Timestamp = rowVersion }).State = EntityState.Deleted;
            return SaveChanges();
        }


        public int Delete(T entity)
        {
            _db.Entry(entity).State = EntityState.Deleted;
            return SaveChanges();
        }

        //could be problem
        public T GetOne(Guid? id) => _table.Find(id);

        public virtual List<T> GetAll() => _table.ToList();

        public List<T> ExecuteQuery(string sql) => _table.FromSqlRaw(sql).ToList();

        public List<T> ExecuteQuery(string sql, object[] sqlParametersObjects)
            => _table.FromSqlRaw(sql, sqlParametersObjects).ToList();

        internal int SaveChanges()
        {
            try
            {
                return _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional 
                //details and affected objects
                //for now, just rethrow the exception
                throw;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw;
            }
        }
    }
}
