using IndentityLogic;
using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class AccountController : BaseIdentityController
    {
        private readonly ILogin _login;
        private readonly IRegister _register;

        public AccountController(ILogin login, IRegister register)
        {
            _login = login;
            _register = register;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return HandlerResult(await _login.LoginHandleAsync(loginDto));  
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return HandlerResult(await _register.RegisterHandlerAsync(registerDto));
        }
    }
}
