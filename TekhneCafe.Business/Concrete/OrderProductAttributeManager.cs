using AutoMapper;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class OrderProductAttributeManager : IOrderProductAttributeService
    {
        private readonly IOrderProductAttributeDal _productAttributeService;

        public OrderProductAttributeManager(IOrderProductAttributeDal productAttributeService, IMapper mapper)
        {
            _productAttributeService = productAttributeService;
        }

        public async Task ValidateOrderProductAttributeAsync(OrderProduct orderProduct)
            => await _productAttributeService.ValidateOrderProductAttributesAsync(orderProduct);
    }
}
