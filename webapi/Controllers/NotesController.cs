﻿using Application.DTOs;
using Application.Notes;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotesController : BaseApiController
    {
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 600)]
        public async Task<IActionResult> GetNotes(Guid pageId,
            [FromQuery] RequestDto requestDto)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { PageId = pageId, RequestDto = requestDto }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteDto noteDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { NoteDto = noteDto }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNote(Guid id, NoteDto noteDto)
        {
            noteDto.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { NoteDto = noteDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
