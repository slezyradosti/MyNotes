using Domain.Models;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IUnitRepository : IRepository<Unit>
    {
        public Task<List<Unit>> GetAllFromNotebookAsync(Guid notebookId);

        public Task<Unit> GetOneWithItsPagesAsync(Guid unitId);
    }
}
