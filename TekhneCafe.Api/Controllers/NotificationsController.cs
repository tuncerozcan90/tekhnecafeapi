using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Filters.Notification;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Notifications([FromQuery] NotificationRequestFilter filters)
        {
            var notifications = _notificationService.GetNotifications(filters);
            return Ok(notifications);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmNotification([FromBody] string id)
        {
            await _notificationService.ConfirmNotification(id);
            return Ok();
        }
    }
}
