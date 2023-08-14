using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    }
}
