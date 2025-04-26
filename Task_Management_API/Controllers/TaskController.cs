using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Taskkss.Commands.Models;
using Task_Management_Core.Features.Taskkss.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : AppControllerBase
    {
        /*[HttpPost("AddTask")]*/
        [HttpPost]
        public async Task<IActionResult> AddTaskAsync([FromBody] AddTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        /*[HttpPut("EditTask")]*/
        [HttpPut]
        public async Task<IActionResult> EditTaskAsync([FromBody] EditTaskCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        /*[HttpDelete("DeleteTask/{Id}")]*/
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new DeleteTaskCommand(Id));
            return NewResult(result);
        }

        /*[HttpGet("GetTaskById/{Id}")]*/
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new GetTaskByIdQuery(Id));
            return NewResult(result);
        }

        //[HttpGet("all")]
        [HttpGet("tasks")]
        [SwaggerOperation(Summary = "جلب كل التاسكات بتفاصيلها")]
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
        //[HttpGet("google-task")]
        [HttpGet("external-tasks")]
        [SwaggerOperation(Summary = "جلب تاسكات من Google Tasks")]
        public async Task<IActionResult> GetAllTasksForCurrentUser()
        {
            /*var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();*/

            var result = await Mediator.Send(new GetExternalTasksForUserQuery());
            return Ok(result);
        }

        [HttpGet("internal-tasks")]
        [SwaggerOperation(Summary = "جلب التاسكات الداخلية التى أضافها المدير للمستخدم")]
        public async Task<IActionResult> GetTasksForUser()
        {
            var result = await Mediator.Send(new GetInternalTasksForUserQuery());
            return Ok(result);
        }
        //[HttpGet("getTasksForSpecificProject/{id}")]
        [HttpGet("projects/{projectId}")]
        [SwaggerOperation(Summary = "جلب كل التاسكات الخاصة ببروجيكت معين")]
        public async Task<IActionResult> GetTasksForspecificProject(int projectId)
        {
            var result = await Mediator.Send(new GetTasksForSpecificProjectQuery(projectId));
            return Ok(result);
        }

        //[HttpGet("getTasksForUser")]

    }
}
