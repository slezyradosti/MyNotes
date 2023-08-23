using Azure;
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

        public async Task<List<Note>> GetAuthorSFilteredAsync(Guid pageId, Guid authorId, 
            IFilter filter)
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

            //check for author
            var notes = await query.Select(note => new {Note = note, UserId = note.Page.Unit.Notebook.UserId})
                .ToListAsync();

            if (notes.Count > 0 && notes.First().UserId != authorId) return null; //throw new Exception("No access");

            return notes.Select(x => x.Note).ToList();
        }

        public async Task<bool> IfUserHasAccessToTheNotes(Guid pageId, Guid authorId)
        {
            var notesAuthorId = await Context.Notes
                .Where(x => x.PageId == pageId)
                .Select(x => x.Page.Unit.Notebook.UserId)
                .FirstOrDefaultAsync();

            return notesAuthorId == authorId;
        }

        public async Task<bool> IfUserHasAccessToTheNote(Guid noteId, Guid authorId)
        {
            var noteAuthorId = await Context.Notes
                .Where(x => x.Id == noteId)
                .Select(x => x.Page.Unit.Notebook.UserId)
                .FirstOrDefaultAsync();

            return noteAuthorId == authorId;
        }

        public async Task<int> GetOwnedCountAsync(Guid pageId)
        {
            return await Context.Notes
                .Where(note => note.PageId == pageId)
                .CountAsync();
        }
    }
}
