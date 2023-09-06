using TekhneCafe.Business.Models;

namespace TekhneCafe.Business.Abstract
{
    public interface IOneSignalNotificationService
    {
        Task<List<NotificationResponseModel>> GetUserNotifications();
        Task SendToGivenUserAsync(CreateNotificationModel notificationModel, string userId);
    }
}
