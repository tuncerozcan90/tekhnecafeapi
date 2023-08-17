using AutoMapper;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class OrderProductManager : IOrderProductService
    {
        private readonly IOrderProductDal _orderProductDal;

        public OrderProductManager(IOrderProductDal orderProductDal, IMapper mapper)
        {
            _orderProductDal = orderProductDal;
        }

        public async Task ValidateOrderProductsAsync(Order order)
            => await _orderProductDal.ValidateOrderProductsAsync(order);
    }
}
