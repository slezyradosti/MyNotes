using IndentityLogic.Core;
using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using IndentityLogic.Models;
using IndentityLogic.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IndentityLogic
{
    public class Login : ILogin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly IUserHandler _userHandler;

        public Login(UserManager<ApplicationUser> userManager,
            TokenService tokenService, IUserHandler userHandler)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userHandler = userHandler;
        }

        public async Task<AccountResult<UserDto, string>> LoginHandleAsync(LoginDto loginDto)
        {
            if (loginDto == null) return null;

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                //addign claim to user for easier access to his Id
                await _userHandler.AddIdClaimToUserAsync(user.Email);

                var userDto = await _userHandler.AddUserDto(user);

                return new AccountResult<UserDto, string>(userDto, true);
            }

            return new AccountResult<UserDto, string>(isSuccessful: false,
                errors: "Failed to login");
        }
    }
}
