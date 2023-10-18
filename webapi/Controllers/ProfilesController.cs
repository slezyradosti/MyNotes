using Application.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query(id)));
        }
    }
}
