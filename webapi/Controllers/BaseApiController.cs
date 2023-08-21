using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using webapi.Extensions;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return NotFound();
            if (!result.IsSuccess) return BadRequest(result.Error);        
            if (result.IsSuccess && result.Value == null) return NotFound();
            if (result.IsSuccess && result.Value != null) return Ok(result.Value);
            return BadRequest();
        }

        protected ActionResult HandlePagedResult<T>(Result<PageList<T>> result)
        {
            if (result == null) return NotFound();
            if (!result.IsSuccess) return BadRequest(result.Error);
            if (result.IsSuccess && result.Value == null) return NotFound();
            if (result.IsSuccess && result.Value != null)
            {
                Response.AddPaginationHeader(result.Value);
                return Ok(result.Value);
            }
            return BadRequest();
        }
    }
}
