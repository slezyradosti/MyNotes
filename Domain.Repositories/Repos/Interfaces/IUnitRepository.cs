using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        public Task<List<Unit>> GetAllFromNotebookAsync(Guid notebookId);
        public Task<List<Unit>> GetAllFilteredAsync(Guid notebookId, IFilter filter);
        public Task<Unit> GetOneWithItsPagesAsync(Guid unitId);
    }
}
