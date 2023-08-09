namespace TekhneCafe.Core.Exceptions.Cart
{
    public class CartBadRequestException : BadRequestException
    {
        public CartBadRequestException() : base("Role bad request!")
        {

        }

        public CartBadRequestException(string message) : base(message)
        {

        }
    }
}
