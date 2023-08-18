using Microsoft.AspNetCore.Identity;

namespace IndentityLogic.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
