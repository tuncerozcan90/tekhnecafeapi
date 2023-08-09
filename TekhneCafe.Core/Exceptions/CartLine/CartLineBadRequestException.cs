namespace TekhneCafe.Core.Exceptions.CartLine
{
    public class CartLineBadRequestException : BadRequestException
    {
        public CartLineBadRequestException() : base("CartLine bad request!")
        {

        }

        public CartLineBadRequestException(string message) : base(message)
        {

        }
    }
}
