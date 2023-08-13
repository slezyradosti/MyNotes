using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos
{
    public class UnitRepository : BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(DataContext dataContext) : base (dataContext) { }

        public async Task<List<Unit>> GetAllFromNotebookAsync(Guid notebookId)
            => await Context.Units.Where(unit => unit.NotebookId == notebookId).ToListAsync();

        public async Task<Unit> GetOneWithItsPagesAsync(Guid unitId) 
            => await Context.Units
            .Where(unit => unit.Id == unitId)
            .Include(unit => unit.Pages)
            .FirstAsync();
    }
}
