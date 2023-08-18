using Application.DTOs;
using Application.Pages;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PagesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPages(Guid unitId)
        {
            return HandleResult(await Mediator.Send(new List.Query { UnitId = unitId }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(PageDto pageDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { pageDto = pageDto }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPage(Guid id, PageDto pageDto)
        {
            pageDto.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { PageDto = pageDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
