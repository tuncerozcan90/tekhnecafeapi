using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Api.ActionFilters;
using TekhneCafe.Api.Consts;
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
        [TypeFilter(typeof(ValidationFilterAttribute<PaymentDtoValidator, PaymentDto>), Arguments = new object[] { ValidationType.FluentValidation })]
        public async Task<IActionResult> Pay([FromBody] PaymentDto payment)
        {
            await _paymentService.Pay(payment);
            return Ok();
        }
    }
}
