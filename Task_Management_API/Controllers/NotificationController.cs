using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Bases;
using Task_Management_Core.Features.Notificationss.Queries.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class NotificationController : AppControllerBase
    {
        [HttpGet("GetNotificationById/{id}")]
        public async Task<IActionResult> GetNotificationbyIdAsync(int id)
        {
            var result = await Mediator.Send(new GetNotificationByIdQuery(id));
            return NewResult(result);
        }

        [HttpGet("GetNotificationListForUser/{id}")]
        public async Task<IActionResult> GetNotificationListForUserAsync(string id)
        {
            var result = await Mediator.Send(new GetNotificationListQuery(id));
            return NewResult(result);
        }
    }
}
