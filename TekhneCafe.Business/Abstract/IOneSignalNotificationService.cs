using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Business.Models;

namespace TekhneCafe.Business.Abstract
{
    public interface IOneSignalNotificationService
    {
        Task<string> PushNotificationAsync(CreateNotificationModel request, Guid appIs, string apikey);
    }
}
