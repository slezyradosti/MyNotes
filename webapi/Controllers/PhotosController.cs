﻿using Application.Photos;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] Guid noteId, 
            [FromForm] IFormFile file)
        {
            return HandleResult(await Mediator.Send(new Add.Command { NoteId = noteId, File = file }));
        }
    }
}
