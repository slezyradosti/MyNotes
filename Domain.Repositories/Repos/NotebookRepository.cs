using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos.Interfaces;

namespace Domain.Repositories.Repos
{
    public class NotebookRepository : BaseRepository<Notebook>, INotebookRepository
    {
        public NotebookRepository(DataContext dataContext) : base(dataContext)
        {
            
        }
    }
}
