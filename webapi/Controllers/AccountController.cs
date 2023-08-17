using IndentityLogic.DTOs;
using IndentityLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogin _login;

        public AccountController(ILogin login)
        {
            _login = login;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _login.LoginHandleAsync(loginDto);
            
            if (user == null) return Unauthorized();
            return Ok(user);         
        }
    }
}
