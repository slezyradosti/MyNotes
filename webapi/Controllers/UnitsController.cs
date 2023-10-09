using Application.DTOs;
using Application.Units;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class UnitsController : BaseApiController
    {
        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-180")]
        public async Task<IActionResult> GetUnits(Guid nbId, 
            [FromQuery] RequestDto requestDto)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query(nbId, requestDto)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit(UnitDto unit)
        {
            return HandleResult(await Mediator.Send(new Create.Command(unit)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUnit(Guid id, UnitDto unit)
        {
            unit.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(unit)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command(id)));
        }
    }
}
