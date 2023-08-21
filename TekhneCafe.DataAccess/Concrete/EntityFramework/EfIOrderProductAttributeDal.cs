using Microsoft.EntityFrameworkCore;
using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfIOrderProductAttributeDal : EfEntityRepositoryBase<OrderProductAttribute, EfTekhneCafeContext>, IOrderProductAttributeDal
    {
        public EfIOrderProductAttributeDal(EfTekhneCafeContext context) : base(context)
        {

        }

        public async Task ValidateOrderProductAttributesAsync(OrderProduct orderProduct)
        {
            foreach (var orderProductAttribute in orderProduct.OrderProductAttributes.ToList())
            {
                var product = await _dbContext.Product.FirstOrDefaultAsync(_ => _.Id == orderProduct.ProductId);
                if (product != null)
                {
                    orderProductAttribute.Name = product.Name;
                    orderProductAttribute.Price = product.Price;
                }
                else
                    orderProduct.OrderProductAttributes.Remove(orderProductAttribute);
            }
        }
    }
}
