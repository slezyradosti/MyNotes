using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories.Repos
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<List<Note>> GetAllFromSpecificPageAsync(Guid id)
            => await Context.Notes
            .Where(x => x.PageId == id)
            .ToListAsync();
    }
}
