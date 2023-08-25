using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Notification;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.Core.Exceptions.Notification;
using TekhneCafe.Core.Extensions;
using TekhneCafe.Core.Filters.Notification;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Helpers.Transaction;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ITransactionManagement _transactionManagement;

        public NotificationManager(INotificationDal notificationDal, IMapper mapper, IHttpContextAccessor httpContext, ITransactionManagement transactionManagement)
        {
            _notificationDal = notificationDal;
            _mapper = mapper;
            _httpContext = httpContext;
            _transactionManagement = transactionManagement;
        }

        public async Task CreateNotificationAsync(string message, string userId, bool isValid)
        {
            var notification = new Notification()
            {
                AppUserId = Guid.Parse(userId),
                Message = message,
                IsValid = isValid
            };
            await _notificationDal.AddAsync(notification);
        }

        public async Task ConfirmNotification(string id)
        {
            var notification = await GetNotificationById(id);
            if (notification.IsValid)
                return;
            notification.IsValid = true;
            try
            {
                using (var transaction = await _transactionManagement.BeginTransactionAsync())
                {
                    await _notificationDal.UpdateAsync(notification);
                    await CreateNotificationAsync("Ödemenizi onayladınız. Keyifli günler :)", _httpContext.HttpContext.User.ActiveUserId(), true);
                    await _transactionManagement.CommitTransactionAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException();
            }
        }

        public List<NotificationListDto> GetNotifications(NotificationRequestFilter filters = null)
        {
            Guid userId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId());
            var filteredResult = new NotificationFilterService().FilterNotifications(_notificationDal.GetAll(_ => _.AppUserId == userId), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<NotificationListDto>>(filteredResult.ResponseValue);
        }

        public async Task<Notification> GetNotificationById(string id)
        {
            Notification notification = await _notificationDal.GetByIdAsync(Guid.Parse(id));
            if (notification is null)
                throw new NotificationNotFoundException();
            return notification;
        }
    }
}
