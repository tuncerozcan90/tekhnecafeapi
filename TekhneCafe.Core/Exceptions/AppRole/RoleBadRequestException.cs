namespace TekhneCafe.Core.Exceptions.AppRole
{
    public class RoleBadRequestException : BadRequestException
    {
        public RoleBadRequestException() : base("Role bad request!")
        {

        }

        public RoleBadRequestException(string message) : base(message)
        {

        }
    }
}
