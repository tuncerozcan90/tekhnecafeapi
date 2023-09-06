using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Models;

namespace TekhneCafe.Business.Concrete
{
    public class OneSignalNotificationManager : IOneSignalNotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly string _appId;
        private readonly string _restApiKey;
        private readonly RestRequest? _request;
        public OneSignalNotificationManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _appId = _configuration.GetValue<string>("OneSignal:AppId");
            _restApiKey = _configuration.GetValue<string>("OneSignal:RestApiKey");
            _request = new RestRequest("");
            _request.AddHeader("accept", "application/json");
            _request.AddHeader("Authorization", $"Basic {_restApiKey}");
        }

        //todo sonra bakılacak
        public async Task<List<NotificationResponseModel>> GetUserNotifications()
        {
            string tagKey = "user_id";
            string tagValue = "56623193-98F2-4603-0A13-08DBA30F860B";
            var options = new RestClientOptions($"https://onesignal.com/api/v1/notifications?app_id={_appId}&tags=[{{\"key\":\"{tagKey}\",\"relation\":\"=\",\"value\":\"{tagValue}\"}}]");
            var client = new RestClient(options);
            var response = await client.GetAsync(_request);
            var notificationResponse = new List<NotificationResponseModel>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = response.Content;
                var responseObject = JsonConvert.DeserializeObject<JObject>(responseBody);
                if (responseObject.TryGetValue("notifications", out JToken notifications))
                    foreach (var notification in notifications)
                    {
                        long unixTimestamp = notification.Value<long>("completed_at");
                        DateTime completedAt = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).LocalDateTime;
                        notificationResponse.Add(new()
                        {
                            Title = notification["headings"]["en"].Value<string>(),
                            Message = notification["contents"]["en"].Value<string>(),
                            CreatedDate = completedAt
                        });
                    }
                else
                    Console.WriteLine("No notifications found in the response.");
            }
            return notificationResponse;
        }

        public async Task SendToGivenUserAsync(CreateNotificationModel notificationModel, string userId)
        {
            string tagKey = "user_id";
            string tagValue = userId.ToUpper();
            var options = new RestClientOptions("https://onesignal.com/api/v1/notifications");
            var client = new RestClient(options);
            var filters = new
            {
                field = "tag",
                key = tagKey,
                relation = "=",
                value = tagValue
            };
            _request.AddJsonBody(System.Text.Json.JsonSerializer.Serialize(new
            {
                filters = new[] { filters },
                headings = new
                {
                    en = notificationModel.Title
                },
                contents = new
                {
                    en = notificationModel.Content,
                },
                app_id = "b7cb2a8a-9b56-40be-b877-d2bf27e892dc"
            }), false);
            var response = await client.PostAsync(_request);
        }
    }
}
