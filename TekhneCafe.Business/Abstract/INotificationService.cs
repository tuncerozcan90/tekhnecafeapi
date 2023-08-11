using TekhneCafe.Core.DTOs.Notification;
using TekhneCafe.Core.Filters.Notification;

namespace TekhneCafe.Business.Abstract
{
    public interface INotificationService
    {
        List<NotificationListDto> GetNotifications(NotificationRequestFilter filters = null);
        Task<NotificationListDto> GetNotificationByIdAsync(string id);
        Task CreateNotificationAsync(NotificationAddDto notificationAddDto);
        Task RemoveNotificationAsync(string id);
        Task UpdateNotificationAsync(NotificationUpdateDto notificationUpdateDto);
    }
}
