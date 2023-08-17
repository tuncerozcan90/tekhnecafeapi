using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface IOrderProductAttributeService
    {
        Task ValidateOrderProductAttributeAsync(OrderProduct orderProduct);
    }
}
