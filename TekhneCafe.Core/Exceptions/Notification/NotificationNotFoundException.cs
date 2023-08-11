namespace TekhneCafe.Core.Exceptions.Notification
{
    public class NotificationNotFoundException : NotFoundException
    {
        public NotificationNotFoundException() : base("Notification not found exception!")
        {

        }

        public NotificationNotFoundException(string message) : base(message)
        {

        }
    }
}
