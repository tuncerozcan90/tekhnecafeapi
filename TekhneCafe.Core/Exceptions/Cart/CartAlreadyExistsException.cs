namespace TekhneCafe.Core.Exceptions.Cart
{
    public class CartAlreadyExistsException : BadRequestException
    {
        public CartAlreadyExistsException() : base("Cart already exists!")
        {

        }

        public CartAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
