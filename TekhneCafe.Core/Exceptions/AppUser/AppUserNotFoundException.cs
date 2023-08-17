namespace TekhneCafe.Core.Exceptions.AppUser
{
    public class AppUserNotFoundException : NotFoundException
    {
        public AppUserNotFoundException() : base("AppUser not found exception!")
        {

        }

        public AppUserNotFoundException(string message) : base(message)
        {

        }
    }
}
