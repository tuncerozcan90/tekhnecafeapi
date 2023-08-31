using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Api.ActionFilters;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.ValidationRules.FluentValidations.Payment;
using TekhneCafe.Core.Consts;
using TekhneCafe.Core.DTOs.Payment;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConsts.CafeAdmin}, {RoleConsts.CafeService}")]
        [TypeFilter(typeof(FluentValidationFilterAttribute<PaymentDtoValidator, PaymentDto>), Arguments = new object[] { "payment" })]
        public async Task<IActionResult> Pay([FromBody] PaymentDto payment)
        {
            await _paymentService.PayAsync(payment);
            return Ok();
        }

        [HttpGet("confirm")]
        [TypeFilter(typeof(ModelValidationFilterAttribute), Arguments = new object[] { "id" })]
        public async Task<IActionResult> ConfirmPayment([FromQuery] string id)
        {
            await _paymentService.ConfirmPaymentAsync(id);
            return Ok();
        }
    }
}
