using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.DTOs;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Domain.Repositories.Repos
{
    public class PageRepository : BaseRepository<Page>, IPageRepository
    {
        public PageRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Page>> GetAllFromOneUnitAsync(Guid unitId)
            => await Context.Pages.Where(page => page.UnitId == unitId).ToListAsync();

        public async Task<List<Page>> GetAllFilteredAsync(Guid unitId, IFilter filter)
        {
            var query = Context.Pages.AsQueryable()
                .Where(page => page.UnitId == unitId)
                .OrderBy($"{filter.SortColumn} {filter.SortOrder}")
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize);

            if (!string.IsNullOrEmpty(filter.FilterQuery))
            {
                query = query.Where(x => x.Name.Contains(filter.FilterQuery));
            }

            return await query.ToListAsync();
        }

        public async Task<Page> GetOneWithItsNotesAsync(Guid id) 
            => await Context.Pages
            .Where(page => page.Id == id)
            .Include(page => page.Notes)
            .FirstAsync();
    }
}
