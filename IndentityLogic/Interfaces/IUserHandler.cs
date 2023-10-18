using IndentityLogic.Core;
using IndentityLogic.DTOs;
using IndentityLogic.Models;
using System.Security.Claims;

namespace IndentityLogic.Interfaces
{
    public interface IUserHandler
    {
        public Task<AccountResult<UserDto, string>> GetCurrentUserAsync(ClaimsPrincipal claimsUser);
        public Task AddIdClaimToUserAsync(string email);
        public Task<UserDto> AddUserDto(ApplicationUser user);
    }
}
