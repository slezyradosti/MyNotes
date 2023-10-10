using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;
using DbUpdateException = Microsoft.EntityFrameworkCore.DbUpdateException;

namespace Domain.Repositories.Repos
{
    public class PhotoRepository : IDisposable, IPhotoRepository
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<Photo> _photoTable;

        public PhotoRepository() : this(new DataContext())
        {
        }
        public PhotoRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _photoTable = dbContext.Set<Photo>();
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
        public async Task<int> AddAsync(Photo photo)
        {
            _photoTable.Add(photo);
            return await SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<Photo> photos)
        {
            _photoTable.AddRange(photos);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsStateAsync(string id, byte[] rowVersion)
        {
            _dbContext.Entry(new Photo() { Id = id }).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsStateAsync(Photo photo)
        {
            _dbContext.Entry(photo).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<List<Photo>> ExecuteQueryAsync(string sqlQuery) 
            => await _photoTable.FromSqlRaw(sqlQuery).ToListAsync();

        public async Task<List<Photo>> ExecuteQueryAsync(string sqlQuery, object[] sqlParametersObjects)
            => await _photoTable.FromSqlRaw(sqlQuery, sqlParametersObjects).ToListAsync();

        public async Task<List<Photo>> GetAllAsync() => await _photoTable.ToListAsync();

        public async Task<int> GetCountAsync() => await _photoTable.CountAsync();

        public async Task<Photo> GetOneAsync(string? id) => await _photoTable.FindAsync(id);

        public async Task<int> RemoveAsync(Photo photo)
        {
            _photoTable.Remove(photo);
            return await SaveChangesAsync();
        }

        public async Task<int> SaveAsync(Photo photo)
        {
            _dbContext.Entry(photo).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        internal async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Thrown when there is a concurrency error
                //for now, just rethrow the exception
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                //Thrown when database update fails
                //Examine the inner exception(s) for additional 
                //details and affected objects
                //for now, just rethrow the exception
                throw ex;
            }
            catch (CommitFailedException ex)
            {
                //handle transaction failures here
                //for now, just rethrow the exception
                throw ex;
            }
            catch (Exception ex)
            {
                //some other exception happened and should be handled
                throw ex;
            }
        }
    }
}
