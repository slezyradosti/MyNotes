using Application.Notebooks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditNotebook(Guid id, Notebook notebook)
        {
            notebook.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Notebook = notebook }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotebook(Guid id)
        {
            return Ok(await Mediator.Send(new  Delete.Command { Id = id }));
        }
    }
}
