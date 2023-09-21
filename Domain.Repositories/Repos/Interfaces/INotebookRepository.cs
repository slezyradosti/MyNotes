using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface INotebookRepository : IRepository<Notebook>
    {
        public Task<Notebook> DetailsAsync(Guid id);
        public Task<List<Notebook>> GetUsersAllFilteredAsync(Guid authorId, IFilter filter);
        public Task<bool> IfUserHasAccessToTheNotebook(Guid notebookId, Guid authorId);
        public Task<int> GetOwnedCountAsync(Guid authorId);
    }
}
