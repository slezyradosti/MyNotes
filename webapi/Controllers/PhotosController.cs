using Application.Photos;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPhotos(Guid noteId)
        {
            return HandleResult(await Mediator.Send(new List.Query(noteId)));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] Guid noteId, 
            [FromForm] IFormFile file)
        {
            return HandleResult(await Mediator.Send(new Add.Command (noteId, file )));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command(id)));
        }
    }
}
