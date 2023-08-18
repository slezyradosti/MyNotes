using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class AccountController : BaseIdentityController
    {
        private readonly ILogin _login;
        private readonly IRegister _register;
        private readonly IUserHandler _userHandler;

        public AccountController(ILogin login, IRegister register,
            IUserHandler userHandler)
        {
            _login = login;
            _register = register;
            _userHandler = userHandler;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return HandleResult(await _login.LoginHandleAsync(loginDto));  
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return HandleResult(await _register.RegisterHandlerAsync(registerDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetCurretUser()
        {
            return HandleResult(await _userHandler.GetCurrentUserAsync(User));
        }
    }
}
