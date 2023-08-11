namespace TekhneCafe.Core.Exceptions.CartLine
{
    public class CartLineInternalServerError : InternalServerErrorException
    {
        public CartLineInternalServerError() : base("A server side error occured with CartLine transaction!")
        {

        }

        public CartLineInternalServerError(string message) : base(message)
        {

        }
    }
}
