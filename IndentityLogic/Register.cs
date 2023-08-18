using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using IndentityLogic.Services;
using Microsoft.AspNetCore.Identity;

namespace IndentityLogic
{
    public class Register : IRegister
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public Register(UserManager<ApplicationUser> userManager, 
            TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
                var userDto = new UserDto
                {
                    DisplayName = user.DisplayName,
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user)
                };

                return new AccountResult<UserDto, IEnumerable<string>>(userDto, true);
            }

            return new AccountResult<UserDto, IEnumerable<string>>(isSuccessful: false, 
                errors: result.Errors.Select(x => x.Description).ToList());          
        }
    }
}
