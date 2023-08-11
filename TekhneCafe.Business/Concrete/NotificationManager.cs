using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public async Task<Notification> GetNotificationByIdAsync(Guid notificationId)
        {
            return await _notificationDal.GetByIdAsync(notificationId);
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            await _notificationDal.AddAsync(notification);
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            await _notificationDal.UpdateAsync(notification);
        }

        public async Task DeleteNotificationAsync(Guid notificationId)
        {
            var notification = await _notificationDal.GetByIdAsync(notificationId);
            if (notification != null)
            {
                await _notificationDal.HardDeleteAsync(notification);
            }
        }

        public Task<List<Notification>> GetAllNotificationsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
