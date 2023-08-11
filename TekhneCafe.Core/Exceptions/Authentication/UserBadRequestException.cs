namespace TekhneCafe.Core.Exceptions.Authentication
{
    public class UserBadRequestException : BadRequestException
    {
        public UserBadRequestException() : base("User bad request!")
        {

        }

        public UserBadRequestException(string message) : base(message)
        {

        }
    }
}
