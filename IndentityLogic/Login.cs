using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IndentityLogic
{
    public class Login : ILogin
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Login(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
                    Token = "to do",
                    Username = user.UserName
                };
            }

            return null;
        }
    }
}
