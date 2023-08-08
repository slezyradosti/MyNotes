using Domain.Models;
using System.Data.Entity;

namespace Domain.Repositories.Repos
{
    public class NoteRepository : BaseRepository<Note>
    {
        //public override List<Note> GetAllFromSpecificPage(string id)
        //    => Context.Notes.Where(x => x.Page.Id == id);
    }
}
