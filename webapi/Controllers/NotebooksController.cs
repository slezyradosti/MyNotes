using Application.Notebooks;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotebooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotebooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Notebook>> List()
        {
            return await _mediator.Send(new List.Query());
        }
    }
}
