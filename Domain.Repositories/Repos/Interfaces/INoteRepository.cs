using Domain.Models;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        public Task<List<Note>> GetAllFromSpecificPageAsync(Guid id);
    }
}
