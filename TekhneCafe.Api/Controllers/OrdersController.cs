using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Order;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderAddDto orderDto)
        {
            await _orderService.CreateOrderAsync(orderDto);
            return Ok();
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmOrder([FromQuery] string id)
        {
            await _orderService.ConfirmOrderAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Orders([FromQuery] string id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
