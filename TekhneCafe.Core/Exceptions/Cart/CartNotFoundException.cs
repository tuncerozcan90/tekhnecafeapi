namespace TekhneCafe.Core.Exceptions.Cart
{
    public class CartNotFoundException : NotFoundException
    {
        public CartNotFoundException() : base("Cart not found exception!")
        {

        }

        public CartNotFoundException(string message) : base(message)
        {

        }
    }
}
