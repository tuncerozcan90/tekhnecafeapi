namespace TekhneCafe.Core.Exceptions.Authentication
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User not found!")
        {

        }

        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
