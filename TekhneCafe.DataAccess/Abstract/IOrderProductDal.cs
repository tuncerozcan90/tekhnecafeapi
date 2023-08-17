using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Abstract
{
    public interface IOrderProductDal
    {
        Task ValidateOrderProductsAsync(Order order);
    }
}
