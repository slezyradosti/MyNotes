using IndentityLogic.Core;
using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using IndentityLogic.Models;
using IndentityLogic.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IndentityLogic
{
    public class UserHandler : IUserHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public UserHandler(UserManager<ApplicationUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AccountResult<UserDto, string>> GetCurrentUserAsync(ClaimsPrincipal claimsUser)
        {
            var user = await _userManager.FindByEmailAsync(claimsUser.FindFirstValue(ClaimTypes.Email));

            if (user != null)
            {
                var userDto = new UserDto
                {
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };

                return new AccountResult<UserDto, string>(userDto, true);
            }

            return new AccountResult<UserDto, string>(isSuccessful: false,
                errors: "Failed to find the user");
        }

        public async Task AddIdClaimToUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userClaims = await _userManager.GetClaimsAsync(user);

            if (!userClaims.Any(x => x.Type == "UserId"))
            {
                await _userManager.AddClaimAsync(user, new Claim("UserId", user.Id.ToString().ToLower()));
            }
        }
    }
}
