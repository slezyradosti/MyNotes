using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IPageRepository : IRepository<Page>
    {
        public Task<List<Page>> GetAllFromOneUnitAsync(Guid unitId);
        public Task<List<Page>> GetAllFilteredAsync(Guid unitId, IFilter filter);
        public Task<Page> GetOneWithItsNotesAsync(Guid id);
        public Task<bool> IfUserHasAccessToThePages(Guid unitId, Guid authorId);
        public Task<bool> IfUserHasAccessToThePage(Guid pageId, Guid authorId);
        public Task<int> GetOwnedCountAsync(Guid unitId);
    }
}
