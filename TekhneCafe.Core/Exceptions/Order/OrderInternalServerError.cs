namespace TekhneCafe.Core.Exceptions.Order
{
    public class OrderInternalServerError : InternalServerErrorException
    {
        public OrderInternalServerError() : base("Beklenmeyen bir hata oluştu!")
        {

        }

        public OrderInternalServerError(string message) : base(message)
        {

        }
    }
}
