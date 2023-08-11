using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class OrderHistoryManager : IOrderHistoryService
    {
        private readonly IOrderHistoryDal _orderHistoryDal;

        public OrderHistoryManager(IOrderHistoryDal orderHistoryDal)
        {
            _orderHistoryDal = orderHistoryDal;
        }

        public async Task CreateOrderHistoryAsync(OrderHistory orderHistory)
        {
            await _orderHistoryDal.AddAsync(orderHistory);
        }

        public async Task DeleteOrderHistoryAsync(Guid orderHistoryId)
        {
            var orderHistory = await _orderHistoryDal.GetByIdAsync(orderHistoryId);
            if (orderHistory != null)
            {
                await _orderHistoryDal.HardDeleteAsync(orderHistory);
            }
        }

        public async Task<List<OrderHistory>> GetAllOrderHistoryAsync()
        {
            return await _orderHistoryDal.GetAll().ToListAsync();
        }

        public async Task<OrderHistory> GetOrderHistoryByIdAsync(Guid orderHistoryId)
        {
            return await GetOrderHistoryByIdAsync(orderHistoryId);
        }

        public async Task UpdateOrderHistoryAsync(OrderHistory orderHistory)
        {
            await UpdateOrderHistoryAsync(orderHistory);
        }
    }
}
