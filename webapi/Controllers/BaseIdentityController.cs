using IndentityLogic.Core;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseIdentityController : ControllerBase
    {
        protected ActionResult HandleResult<T, E>(AccountResult<T, E> result, ILogger? logger = null)
        {
            if (result == null)
            {
                LogInfo(logger, "Unauthorized");
                return Unauthorized();
            }
            if (result.IsSuccessful && result.Value != null)
            {
                LogInfo(logger, "Successed");
                return Ok(result.Value);
            }

            LogInfo(logger, result.Errors.ToString());
            return BadRequest(result.Errors);
        }

        private void LogInfo(ILogger? logger, string info)
        {
            if (logger == null) return;
            logger.LogInformation($"{DateTime.UtcNow}: {info}");
        }
    }
}
