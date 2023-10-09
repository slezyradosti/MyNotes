using Application.DTOs;
using Application.Units;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class UnitsController : BaseApiController
    {
        private readonly ILogger<UnitsController> _logger;

        public UnitsController(ILogger<UnitsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Any-60")]
        public async Task<IActionResult> GetUnits(Guid nbId, 
            [FromQuery] RequestDto requestDto)
        {
            LogInfo("GetUnits action executed");
            return HandlePagedResult(await Mediator.Send(new List.Query(nbId, requestDto)), _logger);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit(Guid id)
        {
            LogInfo("GetUnit action executed");
            return HandleResult(await Mediator.Send(new Details.Query(id)), _logger);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit(UnitDto unit)
        {
            LogInfo("CreateUnit action executed");
            return HandleResult(await Mediator.Send(new Create.Command(unit)), _logger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditUnit(Guid id, UnitDto unit)
        {
            LogInfo("EditUnit action executed");
            unit.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command(unit)), _logger);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            LogInfo("DeleteUnit action executed");
            return HandleResult(await Mediator.Send(new Delete.Command(id)), _logger);
        }

        private void LogInfo(string info)
        {
            _logger?.LogInformation($"{DateTime.UtcNow}: {info}");
        }
    }
}
