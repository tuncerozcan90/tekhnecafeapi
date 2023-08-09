namespace TekhneCafe.Core.Exceptions.Order
{
    public class OrderBadRequestException : BadRequestException
    {
        public OrderBadRequestException() : base("Order bad request!")
        {

        }

        public OrderBadRequestException(string message) : base(message)
        {

        }
    }
}
