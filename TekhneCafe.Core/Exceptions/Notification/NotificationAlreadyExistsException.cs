namespace TekhneCafe.Core.Exceptions.Notification
{
    public class NotificationAlreadyExistsException : BadRequestException
    {
        public NotificationAlreadyExistsException() : base("Notification already exists!")
        {

        }

        public NotificationAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
