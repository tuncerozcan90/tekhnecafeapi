using Microsoft.EntityFrameworkCore;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class OrderManager
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderDal.GetByIdAsync(orderId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderDal.GetAll().ToListAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orderDal.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderDal.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _orderDal.GetByIdAsync(orderId);
            if (order != null)
            {
                await _orderDal.HardDeleteAsync(order);
            }
        }
    }
}
