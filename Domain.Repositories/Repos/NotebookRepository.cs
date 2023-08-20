﻿using Domain.Models;
using Domain.Repositories.EFInitial;
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

        public async Task<List<Notebook>> GetAllFilteredAsync(int pageIndex, int pageSize, 
            string sortColumn, string sortOrder, string? filter)
        {
            var query = Context.Notebooks.AsQueryable()
                .OrderBy($"{sortColumn} {sortOrder}") // to-do: change order filter
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter));
            }

            return await query.ToListAsync();
        }
    }
}
