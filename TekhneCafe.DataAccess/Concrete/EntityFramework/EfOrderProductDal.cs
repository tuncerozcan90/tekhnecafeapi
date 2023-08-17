using Microsoft.EntityFrameworkCore;
using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfOrderProductDal : EfEntityRepositoryBase<OrderProductAttribute, EfTekhneCafeContext>, IOrderProductDal
    {
        private readonly IOrderProductAttributeDal _orderProductAttributeDal;

        public EfOrderProductDal(IOrderProductAttributeDal orderProductAttributeDal)
        {
            _orderProductAttributeDal = orderProductAttributeDal;
        }

        public async Task ValidateOrderProductsAsync(Order order)
        {
            foreach (var orderProduct in order.OrderProducts.ToList())
            {
                var product = await _dbContext.Product.FirstOrDefaultAsync(_ => _.Id == orderProduct.ProductId);
                if (product != null)
                {
                    orderProduct.Name = product.Name;
                    orderProduct.Price = product.Price;
                    orderProduct.Description = product.Description;
                }
                else
                {
                    order.OrderProducts.Remove(orderProduct);
                    continue;
                }
                await _orderProductAttributeDal.ValidateOrderProductAttributesAsync(orderProduct);
            }
        }
    }
}
