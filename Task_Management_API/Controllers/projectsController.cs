using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Projectss.Commands.Models;
using Task_Management_Core.Features.Projectss.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class projectsController : AppControllerBase
    {
        //[HttpPost("AddProject")]
        [HttpPost]
        public async Task<IActionResult> AddProjectAsync([FromBody] AddProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //[HttpPut("EditProject")]
        [HttpPut]
        public async Task<IActionResult> EditProjectAsync([FromBody] EditProjectCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        //[HttpDelete("DeleteProject/{Id}")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProjectAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new DeleteProjectCommand(Id));
            return NewResult(result);
        }

        //[HttpGet("GetProjectById/{Id}")]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProjectByIdAsync([FromRoute] int Id)
        {
            var result = await Mediator.Send(new GetProjectByIdQuery(Id));
            return NewResult(result);
        }

        //[HttpGet("GetProjectsPaginated")]
        [HttpGet]
        public async Task<IActionResult> GetProjectsPaginatedAsync([FromQuery] GetProjectsPaginatedQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
