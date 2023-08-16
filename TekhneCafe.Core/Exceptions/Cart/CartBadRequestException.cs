namespace TekhneCafe.Core.Exceptions.Cart
{
    public class CartBadRequestException : BadRequestException
    {
        public CartBadRequestException() : base("CartCart bad request!")
        {

        }

        public CartBadRequestException(string message) : base(message)
        {

        }
    }
}
