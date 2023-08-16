using Microsoft.AspNetCore.Identity;

namespace IndentityLogic
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
