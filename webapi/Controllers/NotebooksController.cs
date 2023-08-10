using Application.Notebooks;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class NotebooksController : BaseApiController
    {
        [HttpGet]
        public async Task<List<Notebook>> List()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}
