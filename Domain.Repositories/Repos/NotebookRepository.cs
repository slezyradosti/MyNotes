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

        public async Task<List<Notebook>> GetUsersAllFilteredAsync(Guid authorId, IFilter filter)
        {
            var query = Context.Notebooks.AsQueryable()
                .Where(x => x.UserId == authorId)
                .OrderBy($"{filter.SortColumn} {filter.SortOrder}")
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize);

            if (!string.IsNullOrEmpty(filter.FilterQuery))
            {
                query = query.Where(x => x.Name.Contains(filter.FilterQuery));
            }

            return await query.ToListAsync();
        }

        public async Task<bool> IfUserHasAccessToTheNotebook(Guid notebookId, Guid authorId)
        {
            var notebookUserId = await Context.Notebooks
                .Where(x => x.Id == notebookId)
                .Select(x => x.UserId)
                .FirstOrDefaultAsync();

            return notebookUserId == authorId;
        }

        public async Task<int> GetOwnedCountAsync(Guid authorId)
        {
            return await Context.Notebooks
                .Where(x => x.UserId == authorId)
                .CountAsync();
        }
    }
}
