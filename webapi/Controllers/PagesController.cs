using Application.DTOs;
using Application.Pages;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PagesController : BaseApiController
    {
        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-60")]
        public async Task<IActionResult> GetPages(Guid unitId,
            [FromQuery] RequestDto requestDto)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query(unitId, requestDto)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(PageDto pageDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command(pageDto)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPage(Guid id, PageDto pageDto)
        {
            pageDto.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(pageDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command(id)));
        }
    }
}
