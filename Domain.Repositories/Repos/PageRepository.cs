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

        public async Task<bool> IfUserHasAccessToThePages(Guid unitId, Guid authorId)
        {
            var pagesAuthorId = await Context.Pages
                .Where(page => page.UnitId == unitId)
                .Select(page => page.Unit.Notebook.UserId)
                .FirstOrDefaultAsync();

            return pagesAuthorId == authorId;
        }

        public async Task<bool> IfUserHasAccessToThePage(Guid pageId, Guid authorId)
        {
            var pageAuthorId = await Context.Pages
                .Where(page => page.Id == pageId)
                .Select(page => page.Unit.Notebook.UserId)
                .FirstOrDefaultAsync();

            return pageAuthorId == authorId;
        }

        public async Task<int> GetOwnedCountAsync(Guid unitId)
        {
            return await Context.Pages
                .Where(page => page.UnitId == unitId)
                .CountAsync();
        }
    }
}
