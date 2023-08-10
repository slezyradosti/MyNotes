using Application.Notebooks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotebooksController : BaseApiController
    {
        [HttpGet]
        public async Task<List<Notebook>> GetNotebooks()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<Notebook> GetNotebook(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotebook(Notebook notebook)
        {
            return Ok(await Mediator.Send(new Create.Command { Notebook = notebook }));
        }
    }
}
