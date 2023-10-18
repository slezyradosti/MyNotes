using IndentityLogic.Models;

namespace Domain.Repositories.Repos.Interfaces
{
    public interface IUserRepository
    {
        Task<int> SaveAsync(ApplicationUser user);
        public Task<ApplicationUser> GetUser(Guid userId);
    }
}
