using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Filess.Commands.Models;
using Task_Management_Core.Features.Filess.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class filesController : AppControllerBase
    {
        //[HttpPost("upload File")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync([FromForm] AddFileCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //[HttpPost("upload File to Google Drive")]
        [HttpPost("googleDrive")]
        [SwaggerOperation(Summary = "تخزين ملف على Google Drive", OperationId = "UploadFileToGoogleDrive"
)]
        public async Task<IActionResult> UploadFileToGoogleDriveAsync([FromForm] AddFileToGoogleDriveCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //[HttpPut("Edit file")]
        [HttpPut]
        public async Task<IActionResult> EditFileAsync([FromForm] EditFileCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //[HttpPut("Edit file on google drive")]
        [HttpPut("googleDrive")]
        [SwaggerOperation(Summary = "تعديل ملف موجود على Google Drive", OperationId = "EditFileOnGoogleDrive")]
        public async Task<IActionResult> EditFileOnGoogleDriveAsync([FromForm] EditFileInGoogleDriveCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //[HttpDelete("DeleteFile/{Id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileAsync([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteFileCommand(id));
            return NewResult(result);
        }

        //[HttpGet("GetFileById/{Id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileByIdAsync([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetFileByIdQuery(id));
            return NewResult(result);
        }

        //[HttpGet("GetFilesForTaskId/{Id}")]
        //[HttpGet("{taskId}/p")]
        [HttpGet("tasks/{taskId}/files")]
        [SwaggerOperation(Summary = "جلب كل الملفات الخاصة بتاسك معينة", OperationId = "GetFilesByTaskId")]
        public async Task<IActionResult> GetFileForTaskIdAsync([FromRoute] int taskId)
        {
            var result = await Mediator.Send(new GetAllFilesForTaskQuery(taskId));
            return Ok(result);
        }

    }
}
