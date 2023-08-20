using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.DTOs;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Domain.Repositories.Repos
{
    public class NotebookRepository : BaseRepository<Notebook>, INotebookRepository
    {
        public NotebookRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<Notebook> DetailsAsync(Guid id)
            => await Context.Notebooks.Where(notebook => notebook.Id == id)
            .Include(notebook => notebook.Units)
            .FirstAsync();

        public async Task<Notebook> FullDetailsAsync(Guid id)
            => await Context.Notebooks.Where(notebook => notebook.Id == id)
            .Include(notebook => notebook.Units)
            .ThenInclude(unit => unit.Pages)
            .ThenInclude(page => page.Notes)
            .FirstAsync();

        public async Task<List<Notebook>> GetAllFilteredAsync(IFilter filter)
        {
            var query = Context.Notebooks.AsQueryable()
                .OrderBy($"{filter.SortColumn} {filter.SortOrder}")
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize);

            if (!string.IsNullOrEmpty(filter.FilterQuery))
            {
                query = query.Where(x => x.Name.Contains(filter.FilterQuery));
            }

            return await query.ToListAsync();
        }
    }
}
