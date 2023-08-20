using Domain.Models;
using Domain.Repositories.Repos.DTOs;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IPageRepository : IRepository<Page>
    {
        public Task<List<Page>> GetAllFromOneUnitAsync(Guid unitId);
        public Task<List<Page>> GetAllFilteredAsync(Guid unitId, IFilter filter);
        public Task<Page> GetOneWithItsNotesAsync(Guid id);
    }
}
