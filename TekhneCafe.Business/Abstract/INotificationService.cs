using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.AppRole;
using TekhneCafe.Core.DTOs.Notification;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Notification;
using TekhneCafe.Entity.Concrete;

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
