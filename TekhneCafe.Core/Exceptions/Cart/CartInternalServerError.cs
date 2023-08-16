namespace TekhneCafe.Core.Exceptions.Cart
{
    public class CartInternalServerError : InternalServerErrorException
    {
        public CartInternalServerError() : base("A server side error occured with Cart transaction!")
        {

        }

        public CartInternalServerError(string message) : base(message)
        {

        }
    }
}
