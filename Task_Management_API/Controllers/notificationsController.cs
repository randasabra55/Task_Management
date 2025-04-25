using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Notificationss.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class notificationsController : AppControllerBase
    {
        //[HttpGet("GetNotificationById/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationbyIdAsync(int id)
        {
            var result = await Mediator.Send(new GetNotificationByIdQuery(id));
            return NewResult(result);
        }

        //[HttpGet("GetNotificationListForUser/{id}")]
        [HttpGet("{userId}")]
        [SwaggerOperation(Summary = "جلب كل الإشعارات لليوزر", OperationId = "GetNotificationsForUser")]
        public async Task<IActionResult> GetNotificationListForUserAsync(string userId)
        {
            var result = await Mediator.Send(new GetNotificationListQuery(userId));
            return NewResult(result);
        }
    }
}
