namespace TekhneCafe.Core.Exceptions.CartLine
{
    public class CartLineNotFoundException : NotFoundException
    {
        public CartLineNotFoundException() : base("CartLine not found exception!")
        {

        }

        public CartLineNotFoundException(string message) : base(message)
        {

        }
    }
}
