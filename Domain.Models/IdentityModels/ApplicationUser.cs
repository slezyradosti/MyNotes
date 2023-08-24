using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace IndentityLogic.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public ICollection<Notebook> Notebooks { get; set; } = new List<Notebook>();
    }
}
