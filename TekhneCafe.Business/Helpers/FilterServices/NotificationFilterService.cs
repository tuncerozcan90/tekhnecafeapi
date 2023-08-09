using ECommerce.Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Notification;
using TekhneCafe.Core.ResponseHeaders;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Helpers.FilterServices
{
    public class NotificationFilterService
    {
        public NotificationResponseFilter<List<Notification>> FilterNotifications(IQueryable<Notification> notifications, NotificationRequestFilter filters)
        {
            var filteredNotifications = notifications.Skip(filters.Page * filters.Size).Take(filters.Size).ToList();
            Metadata metadata = new()
            {
                CurrentPage = filters.Page,
                PageSize = filters.Size,
                TotalEntities = notifications.Count(),
                TotalPages = notifications.Count() / filters.Size + 1,
            };
            var header = new CustomHeaders().AddPaginationHeader(metadata);

            return new()
            {
                ResponseValue = filteredNotifications,
                Headers = header
            };
        }
    }
}
