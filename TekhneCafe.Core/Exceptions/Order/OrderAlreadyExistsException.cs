namespace TekhneCafe.Core.Exceptions.Order
{
    public class OrderAlreadyExistsException : BadRequestException
    {
        public OrderAlreadyExistsException() : base("Order already exists!")
        {

        }

        public OrderAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
