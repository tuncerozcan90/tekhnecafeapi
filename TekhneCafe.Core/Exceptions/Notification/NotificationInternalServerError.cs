namespace TekhneCafe.Core.Exceptions.Notification
{
    public class NotificationInternalServerError : InternalServerErrorException
    {
        public NotificationInternalServerError() : base("A server side error occured with Notification transaction!")
        {

        }

        public NotificationInternalServerError(string message) : base(message)
        {

        }
    }
}
