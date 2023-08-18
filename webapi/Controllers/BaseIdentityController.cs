using IndentityLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class BaseIdentityController : ControllerBase
    {
        protected ActionResult HandlerResult<T, E>(AccountResult<T, E> result)
        {
            if (result == null) return Unauthorized();
            if (result.IsSuccessful && result.Value != null) return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
