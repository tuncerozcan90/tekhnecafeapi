﻿using AutoMapper;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class OrderProductManager : IOrderProductService
    {
        private readonly IOrderProductAttributeService _orderProductAttributeService;
        private readonly IProductDal _productDal;

        public OrderProductManager(IOrderProductAttributeService orderProductAttributeService, IProductDal productDal, IMapper mapper)
        {
            _orderProductAttributeService = orderProductAttributeService;
            _productDal = productDal;
        }
        public async Task ValidateOrderProductsAsync(Order order)
        {
            foreach (var orderProduct in order.OrderProducts.ToList())
            {
                var product = await _productDal.GetByIdAsync(orderProduct.ProductId);
                if (product != null)
                {
                    orderProduct.Quantity = orderProduct.Quantity > 0 ? orderProduct.Quantity : 1;
                    orderProduct.Name = product.Name;
                    orderProduct.Price = product.Price;
                    orderProduct.Description = product.Description;
                }
                else
                {
                    order.OrderProducts.Remove(orderProduct);
                    continue;
                }
                await _orderProductAttributeService.ValidateOrderProductAttributesAsync(orderProduct);
            }
        }
    }
}
