using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Order;
using TekhneCafe.Core.Exceptions.Order;
using TekhneCafe.Core.Filters.Order;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public OrderManager(IOrderDal orderDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _orderDal = orderDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task CreateOrderAsync(OrderAddDto orderAddDto)
        {
            Order order = _mapper.Map<Order>(orderAddDto);
            await _orderDal.AddAsync(order);
        }

        public async Task<OrderListDto> GetOrderByIdAsync(string id)
        {
            var order = await GetOrderById(id);
            return _mapper.Map<OrderListDto>(order);
        }

        public List<OrderListDto> GetOrders(OrderRequestFilter filters = null)
        {
            var filteredResult = new OrderFilterService().FilterOrders(_orderDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<OrderListDto>>(filteredResult.ResponseValue);
        }

        public async Task RemoveOrderAsync(string id)
        {
            Order order = await GetOrderById(id);
            await _orderDal.SafeDeleteAsync(order);
        }

        public async Task UpdateOrderAsync(OrderUpdateDto orderUpdateDto)
        {
            Order order = await GetOrderById(orderUpdateDto.Id);
            _mapper.Map(orderUpdateDto, order);
            await _orderDal.UpdateAsync(order);
        }

        private async Task<Order> GetOrderById(string id)
        {
            Order order = await _orderDal.GetByIdAsync(Guid.Parse(id));
            if (order is null)
                throw new OrderNotFoundException();

            return order;
        }
    }
}
