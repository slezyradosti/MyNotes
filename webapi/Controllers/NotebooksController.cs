﻿using Application.DTOs;
using Application.Notebooks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotebooksController : BaseApiController
    {
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 0)]
        public async Task<IActionResult> GetNotebooks([FromQuery] RequestDto request)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query(request)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotebook(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotebook(NotebookDto notebook)
        {
            return HandleResult(await Mediator.Send(new Create.Command(notebook)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNotebook(Guid id, NotebookDto notebook)
        {
            notebook.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(notebook)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotebook(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command(id)));
        }
    }
}
