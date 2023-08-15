using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Cart;

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

        [HttpPost]
        public async Task<IActionResult> CompleteOrder([FromBody] CartAddDto cartAddDto)
        {
            await _orderService.CreateOrderAsync(cartAddDto);
            return Ok();
        }

        //public async Task ConfirmOrder()
        //{

        //}
    }
}
