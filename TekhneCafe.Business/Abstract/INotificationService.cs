using TekhneCafe.Core.DTOs.Notification;
using TekhneCafe.Core.Filters.Notification;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(string message, string userId, bool isValid);
        Task ConfirmNotification(string id);
        List<NotificationListDto> GetNotifications(NotificationRequestFilter filters = null);
        Task<Notification> GetNotificationById(string id);
    }
}
