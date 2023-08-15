using Domain.Models;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IPageRepository : IRepository<Page>
    {
        public Task<List<Page>> GetAllFromOneUnitAsync(Guid unitId);
        public Task<Page> GetOneWithItsNotesAsync(Guid id);
    }
}
