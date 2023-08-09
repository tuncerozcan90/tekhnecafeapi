namespace TekhneCafe.Core.Exceptions.Order
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException() : base("Order not found exception!")
        {

        }

        public OrderNotFoundException(string message) : base(message)
        {

        }
    }
}
