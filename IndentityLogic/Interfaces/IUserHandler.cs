using IndentityLogic.Core;
using IndentityLogic.DTOs;
using System.Security.Claims;

namespace IndentityLogic.Interfaces
{
    public interface IUserHandler
    {
        public Task<AccountResult<UserDto, string>> GetCurrentUserAsync(ClaimsPrincipal claimsUser);
    }
}
