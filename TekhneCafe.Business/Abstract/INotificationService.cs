using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationByIdAsync(Guid notificationId);
        Task<List<Notification>> GetAllNotificationsAsync();
        Task CreateNotificationAsync(Notification notification);
        Task UpdateNotificationAsync(Notification notification);
        Task DeleteNotificationAsync(Guid notificationId);
    }
}
