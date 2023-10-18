using IndentityLogic.Core;
using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using IndentityLogic.Models;
using IndentityLogic.Services;
using Microsoft.AspNetCore.Identity;

namespace IndentityLogic
{
    public class Register : IRegister
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly IUserHandler _userHandler;

        public Register(UserManager<ApplicationUser> userManager,
            TokenService tokenService, IUserHandler userHandler)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _userHandler = userHandler;
        }

        public async Task<AccountResult<UserDto, IEnumerable<string>>> RegisterHandlerAsync(RegisterDto registerDto)
        {
            if (registerDto == null) return null;

            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                //addign claim to user for easier access to his Id
                await _userHandler.AddIdClaimToUserAsync(user.Email);


                var userDto = await _userHandler.AddUserDto(user);

                return new AccountResult<UserDto, IEnumerable<string>>(userDto, true);
            }

            return new AccountResult<UserDto, IEnumerable<string>>(isSuccessful: false,
                errors: result.Errors.Select(x => x.Description).ToList());
        }
    }
}
