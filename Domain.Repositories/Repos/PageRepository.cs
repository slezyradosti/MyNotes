using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos
{
    public class PageRepository : BaseRepository<Page>, IPageRepository
    {
        public PageRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Page>> GetAllFromOneUnitAsync(Guid unitId)
            => await Context.Pages.Where(page => page.UnitId == unitId).ToListAsync();

        public async Task<Page> GetOneWithItsNotesAsync(Guid id) 
            => await Context.Pages
            .Where(page => page.Id == id)
            .Include(page => page.Notes)
            .FirstAsync();
    }
}
