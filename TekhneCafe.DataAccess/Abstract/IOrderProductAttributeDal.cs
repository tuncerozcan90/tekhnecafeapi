using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Abstract
{
    public interface IOrderProductAttributeDal
    {
        Task ValidateOrderProductAttributesAsync(OrderProduct orderProduct);
    }
}
