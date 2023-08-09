namespace TekhneCafe.Core.Exceptions.CartLine
{
    public class CartLineAlreadyExistsException : BadRequestException
    {
        public CartLineAlreadyExistsException() : base("CartLine already exists!")
        {

        }

        public CartLineAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
