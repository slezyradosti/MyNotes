using Application.DTOs;
using Application.Notebooks;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotebooksController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetNotebooks()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotebook(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotebook(NotebookDto notebook)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Notebook = notebook }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNotebook(Guid id, NotebookDto notebook)
        {
            notebook.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Notebook = notebook }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotebook(Guid id)
        {
            return HandleResult(await Mediator.Send(new  Delete.Command { Id = id }));
        }
    }
}
