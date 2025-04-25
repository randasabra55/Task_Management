using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Taskkss.Commands.Models;
using Task_Management_Core.Features.Taskkss.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : AppControllerBase
    {
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTaskAsync([FromBody] AddTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("EditTask")]
        public async Task<IActionResult> EditTaskAsync([FromBody] EditTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("DeleteTask/{Id}")]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new DeleteTaskCommand(Id));
            return NewResult(result);
        }

        [HttpGet("GetTaskById/{Id}")]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new GetTaskByIdQuery(Id));
            return NewResult(result);
        }

        [HttpGet("GetTasksPaginated")]
        public async Task<IActionResult> GetTasksPaginatedAsync([FromQuery] GetTasksPaginatedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        /*[HttpGet("all")]
        public async Task<IActionResult> GetAllTasksForCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await Mediator.Send(new GetUserAllTasksQuery(userId));
            return Ok(result);
        }*/
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasksForCurrentUser()
        {
            /*var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();*/

            var result = await Mediator.Send(new GetUserAllTasksQuery());
            return Ok(result);
        }
    }
}
