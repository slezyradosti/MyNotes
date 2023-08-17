using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using IndentityLogic.Services;
using Microsoft.AspNetCore.Identity;

namespace IndentityLogic
{
    public class Login : ILogin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenService _tokenService;

        public Login(UserManager<ApplicationUser> userManager, 
            TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto> LoginHandleAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return null;

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (result)
            {
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token = _tokenService.CreateToken(user),
                    Username = user.UserName
                };
            }

            return null;
        }
    }
}
