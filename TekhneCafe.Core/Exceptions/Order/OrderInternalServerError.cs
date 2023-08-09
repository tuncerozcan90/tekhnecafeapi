namespace TekhneCafe.Core.Exceptions.Order
{
    public class OrderInternalServerError : InternalServerErrorException
    {
        public OrderInternalServerError() : base("A server side error occured with Order transaction!")
        {

        }

        public OrderInternalServerError(string message) : base(message)
        {

        }
    }
}
