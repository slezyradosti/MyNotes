using Application.DTOs;
using Application.Units;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class UnitsController : BaseApiController
    {
        [HttpGet("nbId")]
        public async Task<IActionResult> GetUnits(Guid nbId)
        {
            return HandleResult(await Mediator.Send(new List.Query { NotebookId = nbId }));
        }

        [HttpGet("{id}&nbId")]
        public async Task<IActionResult> GetUnit(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { UnitId = id }));
        }

        [HttpPost("nbId")]
        public async Task<IActionResult> CreateUnit(Guid nbId, UnitDto unit)
        {
            unit.NotebookId = nbId;
            return HandleResult(await Mediator.Send(new Create.Command { Unit = unit }));
        }

        [HttpPut("{id}&nbId")]
        public async Task<IActionResult> EditUnit(Guid id, Guid nbId, UnitDto unit)
        {
            unit.Id = id;
            unit.NotebookId = nbId;
            return HandleResult(await Mediator.Send(new Edit.Command { Unit = unit }));
        }

        [HttpDelete("{id}&nbId")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
