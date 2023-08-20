using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.DTOs;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Domain.Repositories.Repos
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Note>> GetAllFromSpecificPageAsync(Guid id)
            => await Context.Notes
            .Where(x => x.PageId == id)
            .ToListAsync();

        public async Task<List<Note>> GetAllFilteredAsync(Guid pageId, IFilter filter)
        {
            var query = Context.Notes.AsQueryable()
                .Where(x => x.PageId == pageId)
                .OrderBy($"{filter.SortColumn} {filter.SortOrder}")
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize);

            if (!string.IsNullOrEmpty(filter.FilterQuery))
            {
                query = query.Where(x => x.Record.Contains(filter.FilterQuery));
            }

            return await query.ToListAsync();
        }
    }
}
