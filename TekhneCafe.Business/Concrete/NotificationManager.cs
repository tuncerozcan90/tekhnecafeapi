using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Notification;
using TekhneCafe.Core.Exceptions.Notification;
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

        public async Task CreateNotificationAsync(NotificationAddDto notificationAddDto)
        {
            Notification notification = _mapper.Map<Notification>(notificationAddDto);
            await _notificationDal.AddAsync(notification);
        }

        public async Task<NotificationListDto> GetNotificationByIdAsync(string id)
        {
            var notification = await GetNotificationById(id);
            return _mapper.Map<NotificationListDto>(notification);
        }

        public List<NotificationListDto> GetNotifications(NotificationRequestFilter filters = null)
        {
            var filteredResult = new NotificationFilterService().FilterNotifications(_notificationDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<NotificationListDto>>(filteredResult.ResponseValue);
        }

        public async Task RemoveNotificationAsync(string id)
        {
            Notification notification = await GetNotificationById(id);
            await _notificationDal.SafeDeleteAsync(notification);
        }

        public async Task UpdateNotificationAsync(NotificationUpdateDto notificationUpdateDto)
        {
            Notification notification = await GetNotificationById(notificationUpdateDto.Id);
            _mapper.Map(notificationUpdateDto, notification);
            await _notificationDal.UpdateAsync(notification);
        }
        private async Task<Notification> GetNotificationById(string id)
        {
            Notification notification = await _notificationDal.GetByIdAsync(Guid.Parse(id));
            if (notification is null)
                throw new NotificationNotFoundException();

            return notification;
        }
    }
}
