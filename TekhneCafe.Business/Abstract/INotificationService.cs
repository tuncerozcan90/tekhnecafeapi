using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
