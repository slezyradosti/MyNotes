using IndentityLogic.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseIdentityController : ControllerBase
    {
        protected ActionResult HandleResult<T, E>(AccountResult<T, E> result)
        {
            if (result == null) return Unauthorized();
            if (result.IsSuccessful && result.Value != null) return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
