using Application.DTOs;
using Application.Pages;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PagesController : BaseApiController
    {
        private readonly ILogger<PagesController> _logger;

        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-60")]
        public async Task<IActionResult> GetPages(Guid unitId,
            [FromQuery] RequestDto requestDto)
        {
            LogInfo("GetPages action executed");
            return HandlePagedResult(await Mediator.Send(new List.Query(unitId, requestDto)), _logger);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPage(Guid id)
        {
            LogInfo("GetPage action executed");
            return HandleResult(await Mediator.Send(new Details.Query(id)), _logger);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePage(PageDto pageDto)
        {
            LogInfo("CreatePage action executed");
            return HandleResult(await Mediator.Send(new Create.Command(pageDto)), _logger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPage(Guid id, PageDto pageDto)
        {
            LogInfo("EditPage action executed");
            pageDto.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(pageDto)), _logger);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePage(Guid id)
        {
            LogInfo("DeletePage action executed");
            return HandleResult(await Mediator.Send(new Delete.Command(id)), _logger);
        }

        private void LogInfo(string info)
        {
            _logger?.LogInformation($"{DateTime.UtcNow}: {info}");
        }
    }
}
