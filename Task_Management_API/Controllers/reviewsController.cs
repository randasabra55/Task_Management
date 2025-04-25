using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Reviewss.Commands.Models;
using Task_Management_Core.Features.Reviewss.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class reviewsController : AppControllerBase
    {
        //[HttpPost("AddReview")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        /*[HttpPut("EditReview")]*/
        [HttpPut]
        public async Task<IActionResult> EditReviewAsync([FromBody] EditReviewCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        /*[HttpDelete("DeleteReview/{id:int}")]*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewAsync(int id)
        {
            var result = await Mediator.Send(new DeleteReviewCommand(id));
            return NewResult(result);
        }

        //[HttpGet("GetReviewsPaginatedForSpecificTask/{id}")]
        [HttpGet("{taskId}")]
        [SwaggerOperation(Summary = "جلب الكومنتات لتاسك معين", OperationId = "GetReviewsForTask")]
        public async Task<IActionResult> GetReviewsPaginatedAsync(int taskId)
        {
            var result = await Mediator.Send(new GetReviewsPaginatedAboutTaskQuery(taskId));
            return Ok(result);
        }

        //[HttpGet("GetReviewbyId/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetReviewByIdQuery(id));
            return Ok(result);
        }
    }
}
