using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapi.Extensions;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result, ILogger? logger = null)
        {
            if (result == null)
            {
                LogInfo(logger, "Not found");
                return NotFound();
            }
            if (!result.IsSuccess)
            {
                LogInfo(logger, result.Error);
                return BadRequest(result.Error);
            }
            if (result.IsSuccess && result.Value == null)
            {
                LogInfo(logger, "Not found");
                return NotFound();
            }
            if (result.IsSuccess && result.Value != null)
            {
                LogInfo(logger, "Successed");
                return Ok(result.Value);
            }

            LogInfo(logger, "Bad Request");
            return BadRequest();
        }

        protected ActionResult HandlePagedResult<T>(Result<PageList<T>> result, ILogger? logger = null)
        {
            if (result == null)
            {
                LogInfo(logger, "Not found");
                return NotFound();
            }
            if (!result.IsSuccess)
            {
                LogInfo(logger, result.Error);
                return BadRequest(result.Error);
            }
            if (result.IsSuccess && result.Value == null)
            {
                LogInfo(logger, "Not found");
                return NotFound();
            }
            if (result.IsSuccess && result.Value != null)
            {
                LogInfo(logger, "Successed");
                Response.AddPaginationHeader(result.Value);
                return Ok(result.Value);
            }

            LogInfo(logger, "Bad Request");
            return BadRequest();
        }

        private void LogInfo(ILogger? logger, string info)
        {
            if (logger == null) return;
            logger.LogInformation($"{DateTime.UtcNow}: {info}");
        }
    }
}
