using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface INotebookRepository : IRepository<Notebook>
    {
        public Task<Notebook> DetailsAsync(Guid id);
        public Task<Notebook> FullDetailsAsync(Guid id);
        public Task<List<Notebook>> GetAllFilteredAsync(IFilter filter);
    }
}
