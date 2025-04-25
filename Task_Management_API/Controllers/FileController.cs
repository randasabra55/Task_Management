using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Filess.Commands.Models;
using Task_Management_Core.Features.Filess.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : AppControllerBase
    {
        [HttpPost("upload File")]
        public async Task<IActionResult> UploadFileAsync([FromForm] AddFileCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost("upload File to Google Drive")]
        public async Task<IActionResult> UploadFileToGoogleDriveAsync([FromForm] AddFileToGoogleDriveCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("Edit file")]
        public async Task<IActionResult> EditFileAsync([FromForm] EditFileCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("Edit file on google drive")]
        public async Task<IActionResult> EditFileOnGoogleDriveAsync([FromForm] EditFileInGoogleDriveCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("DeleteFile/{Id}")]
        public async Task<IActionResult> DeleteFileAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new DeleteFileCommand(Id));
            return NewResult(result);
        }

        [HttpGet("GetFileById/{Id}")]
        public async Task<IActionResult> GetFileByIdAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new GetFileByIdQuery(Id));
            return NewResult(result);
        }

        [HttpGet("GetFilesForTaskId/{Id}")]
        public async Task<IActionResult> GetFileForTaskIdAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new GetAllFilesForTaskQuery(Id));
            return Ok(result);
        }

    }
}
