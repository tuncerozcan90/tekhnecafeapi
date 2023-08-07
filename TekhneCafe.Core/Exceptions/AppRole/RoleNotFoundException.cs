namespace TekhneCafe.Core.Exceptions.AppRole
{
    public class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException() : base("Role not found exception!")
        {

        }

        public RoleNotFoundException(string message) : base(message)
        {

        }
    }
}
