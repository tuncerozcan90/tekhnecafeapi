namespace TekhneCafe.Core.Exceptions.Cart
{
    public class CartNotFoundException : NotFoundException
    {
        public CartNotFoundException() : base("Role not found exception!")
        {

        }

        public CartNotFoundException(string message) : base(message)
        {

        }
    }
}
