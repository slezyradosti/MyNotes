using Application.DTOs;
using Application.Notebooks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotebooksController : BaseApiController
    {
        private readonly ILogger<NotebooksController> _logger;

        public NotebooksController(ILogger<NotebooksController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-180")]
        public async Task<IActionResult> GetNotebooks([FromQuery] RequestDto request)
        {
            LogInfo("GetNotebooks action executed");
            return HandlePagedResult(await Mediator.Send(new List.Query(request)), _logger);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotebook(Guid id)
        {
            LogInfo("GetNotebook action executed");
            return HandleResult(await Mediator.Send(new Details.Query(id)), _logger);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotebook(NotebookDto notebook)
        {
            LogInfo("CreateNotebook action executed");
            return HandleResult(await Mediator.Send(new Create.Command(notebook)), _logger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNotebook(Guid id, NotebookDto notebook)
        {
            LogInfo("EditNotebook action executed");
            notebook.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(notebook)), _logger);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotebook(Guid id)
        {
            LogInfo("DeleteNotebook action executed");            
            return HandleResult(await Mediator.Send(new Delete.Command(id)), _logger);
        }

        [HttpGet]
        [Route("UserActionsStat")]
        public async Task<IActionResult> GetUserCreatActionsStat()
        {
            LogInfo("GetUserCreatActionsStat action executed");
            return HandleResult(await Mediator.Send(new UserCreateActionStatistic.Query()), _logger);
        }


        private void LogInfo(string info)
        {
            _logger?.LogInformation($"{DateTime.UtcNow}: {info }");
        }
    }
}
