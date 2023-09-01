using Minio.Exceptions;
using OneSignal.RestAPIv3.Client;
using OneSignal.RestAPIv3.Client.Resources;
using OneSignal.RestAPIv3.Client.Resources.Notifications;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Models;

namespace TekhneCafe.Business.Concrete
{
    public class OneSignalNotificationManager : IOneSignalNotificationService
    {
        public OneSignalNotificationManager()
        {

        }

        public async Task<string> PushNotificationAsync(CreateNotificationModel request, Guid appIs, string apikey)
        {
            var client = new OneSignalClient(apikey);
            var opt = new NotificationCreateOptions()
            {
                AppId = appIs,
                IncludedSegments = new string[] {"Subscribed Users"},
            };
            opt.Headings.Add(LanguageCodes.Turkish, request.Title);
            opt.Contents.Add(LanguageCodes.Turkish, request.Content);
            try
            {
                NotificationCreateResult result = await client.Notifications.CreateAsync(opt);
                return result.Id;
            }
            catch (Exception ex) 
            {
                throw new InternalServerException(ex.Message);
            }
        }
    }
}
