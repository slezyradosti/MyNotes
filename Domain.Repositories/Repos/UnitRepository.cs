using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.DTOs;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Domain.Repositories.Repos
{
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(DataContext dataContext) : base (dataContext) { }

        public async Task<List<Unit>> GetAllFromNotebookAsync(Guid notebookId)
            => await Context.Units.Where(unit => unit.NotebookId == notebookId).ToListAsync();

        public async Task<List<Unit>> GetAllFilteredAsync(Guid notebookId, IFilter filter)
        {
            var query = Context.Units.AsQueryable()
                .Where(unit => unit.NotebookId == notebookId)
                .OrderBy($"{filter.SortColumn} {filter.SortOrder}")
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize);

            if (!string.IsNullOrEmpty(filter.FilterQuery))
            {
                query = query.Where(x => x.Name.Contains(filter.FilterQuery));
            }

            return await query.ToListAsync();
        }

        public async Task<Unit> GetOneWithItsPagesAsync(Guid unitId) 
            => await Context.Units
            .Where(unit => unit.Id == unitId)
            .Include(unit => unit.Pages)
            .FirstAsync();

        public async Task<bool> IfUserHasAccessToTheUnits(Guid notebookId, Guid authorId)
        {
            var unitsAuthorId = await Context.Units
                .Where(unit => unit.NotebookId == notebookId)
                .Select(unit => unit.Notebook.UserId)
                .FirstOrDefaultAsync();

            return unitsAuthorId == authorId;
        }

        public async Task<bool> IfUserHasAccessToTheUnit(Guid unitId, Guid authorId)
        {
            var unitAuthorId = await Context.Units
                .Where(unit => unit.Id == unitId)
                .Select(unit => unit.Notebook.UserId)
                .FirstOrDefaultAsync();

            return unitAuthorId == authorId;
        }

        public async Task<int> GetOwnedCountAsync(Guid notebookId)
        {
            return await Context.Units
                .Where(unit => unit.NotebookId == notebookId)
                .CountAsync();
        }
    }
}
