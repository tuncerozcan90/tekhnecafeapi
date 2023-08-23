namespace TekhneCafe.Core.Exceptions.AppRole
{
    public class RoleAlreadyExistsException : BadRequestException
    {
        public RoleAlreadyExistsException() : base("Role already exists!")
        {

        }

        public RoleAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
