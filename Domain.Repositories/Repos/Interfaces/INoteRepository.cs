using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface INoteRepository : IRepository<Note>
    {
        public Task<List<Note>> GetAllFromSpecificPageAsync(Guid id);
        public Task<List<Note>> GetAllFilteredAsync(Guid pageId, IFilter filter);
        public Task<List<Note>> GetAuthorSFilteredAsync(Guid pageId, Guid authorId,
            IFilter filter);
        public Task<bool> IfUserHasAccessToTheNotes(Guid pageId, Guid authorId);
        public Task<bool> IfUserHasAccessToTheNote(Guid noteId, Guid authorId);
        public Task<int> GetOwnedCountAsync(Guid pageId);
    }
}
