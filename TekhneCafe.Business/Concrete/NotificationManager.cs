using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Notification;
using TekhneCafe.Core.Exceptions.Notification;
using TekhneCafe.Core.Extensions;
using TekhneCafe.Core.Filters.Notification;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public NotificationManager(INotificationDal notificationDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _notificationDal = notificationDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task CreateNotificationAsync(string message, string userId, bool isConfirmed)
        {
            var notification = new Notification()
            {
                AppUserId = Guid.Parse(userId),
                Message = message,
                IsConfirmed = isConfirmed
            };
            await _notificationDal.AddAsync(notification);
        }

        public async Task<bool> ConfirmNotificationAsync(string id)
        {
            var notification = await GetNotificationByIdAsync(id);
            if (notification.IsConfirmed)
                return false;
            notification.IsConfirmed = true;
            await _notificationDal.UpdateAsync(notification);
            return true;
        }

        public List<NotificationListDto> GetNotifications(NotificationRequestFilter filters = null)
        {
            Guid userId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId());
            var filteredResult = new NotificationFilterService().FilterNotifications(_notificationDal.GetAll(_ => _.AppUserId == userId), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<NotificationListDto>>(filteredResult.ResponseValue);
        }

        public async Task<Notification> GetNotificationByIdAsync(string id)
        {
            Notification notification = await _notificationDal.GetByIdAsync(Guid.Parse(id));
            if (notification is null)
                throw new NotificationNotFoundException();
            return notification;
        }
    }
}
