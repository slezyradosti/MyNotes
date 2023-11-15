using Application.DTOs;
using Application.Notes;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotesController : BaseApiController
    {
        private readonly ILogger<NotesController> _logger;

        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "NoCache")]
        public async Task<IActionResult> GetNotes(Guid pageId,
            [FromQuery] RequestDto requestDto)
        {
            LogInfo("GetNotes action executed");
            return HandlePagedResult(await Mediator.Send(new List.Query(pageId, requestDto)), _logger);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            LogInfo("GetNote action executed");
            return HandleResult(await Mediator.Send(new Details.Query(id)), _logger);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteDto noteDto)
        {
            LogInfo($" CreateNote action executed");
            return HandleResult(await Mediator.Send(new Create.Command(noteDto)), _logger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNote(Guid id, NoteDto noteDto)
        {
            LogInfo("EditNote action executed");
            noteDto.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(noteDto)), _logger);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            LogInfo("DeleteNote action executed");
            return HandleResult(await Mediator.Send(new Delete.Command(id)), _logger);
        }

        [HttpGet]
        [Route("UserActionsStat")]
        public async Task<IActionResult> GetUserCreatActionsStat()
        {
            LogInfo("GetUserCreatActionsStat action executed");
            return HandleResult(await Mediator.Send(new UserCreatedActionStatistic.Query()), _logger);
        }

        private void LogInfo(string info)
        {
            _logger?.LogInformation($"{DateTime.UtcNow}: {info}");
        }
    }
}
