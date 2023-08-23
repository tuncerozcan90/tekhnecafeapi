namespace TekhneCafe.Core.Exceptions.AppRole
{
    public class RoleInternalServerError : InternalServerErrorException
    {
        public RoleInternalServerError() : base("A server side error occured with role transaction!")
        {

        }

        public RoleInternalServerError(string message) : base(message)
        {

        }
    }
}
