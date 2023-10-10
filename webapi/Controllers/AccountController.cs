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
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogin login, IRegister register,
            IUserHandler userHandler, ILogger<AccountController> logger)
        {
            _login = login;
            _register = register;
            _userHandler = userHandler;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            LogInfo($"User {loginDto.Email} is loginning");
            return HandleResult(await _login.LoginHandleAsync(loginDto), _logger);  
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            LogInfo($"User {registerDto.Username} {registerDto.Email} is registering");
            return HandleResult(await _register.RegisterHandlerAsync(registerDto), _logger);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurretUser()
        {
            return HandleResult(await _userHandler.GetCurrentUserAsync(User));
        }

        private void LogInfo(string info)
        {
            _logger?.LogInformation($"{DateTime.UtcNow}: {info}");
        }
    }
}
