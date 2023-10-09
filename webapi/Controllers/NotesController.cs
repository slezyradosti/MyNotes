using Application.DTOs;
using Application.Notes;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotesController : BaseApiController
    {
        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-30")]
        public async Task<IActionResult> GetNotes(Guid pageId,
            [FromQuery] RequestDto requestDto)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query(pageId, requestDto)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteDto noteDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command(noteDto)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNote(Guid id, NoteDto noteDto)
        {
            noteDto.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(noteDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command(id)));
        }
    }
}
