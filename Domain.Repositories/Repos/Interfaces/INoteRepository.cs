using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        public Task<List<Note>> GetAllFromSpecificPageAsync(Guid id);
        public Task<List<Note>> GetAllFilteredAsync(Guid pageId, IFilter filter);
    }
}
