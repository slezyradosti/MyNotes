using Domain.Models;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface INotebookRepository : IRepository<Notebook>
    {
        public Task<Notebook> DetailsAsync(Guid id);
        public Task<Notebook> FullDetailsAsync(Guid id);
        public Task<List<Notebook>> GetAllFilteredAsync(int pageIndex, int pageSize,
            string sortColumn, string sortOrder, string? filter);
    }
}
