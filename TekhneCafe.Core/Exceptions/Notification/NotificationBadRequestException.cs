namespace TekhneCafe.Core.Exceptions.Notification
{
    public class NotificationBadRequestException : BadRequestException
    {
        public NotificationBadRequestException() : base("Notification bad request!")
        {

        }

        public NotificationBadRequestException(string message) : base(message)
        {

        }
    }
}
