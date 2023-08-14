using Application.DTOs;
using Application.Units;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { UnitId = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit(UnitDto unit)
        {
            //unit.NotebookId = nbId;
            return HandleResult(await Mediator.Send(new Create.Command { Unit = unit }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUnit(Guid id, UnitDto unit)
        {
            unit.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Unit = unit }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
