using FluentValidation;
using TekhneCafe.Core.DTOs.Order;

namespace TekhneCafe.Business.ValidationRules.FluentValidations.Order
{
    public class OrderAddDtoValidator : AbstractValidator<OrderAddDto>
    {
        public OrderAddDtoValidator()
        {
            RuleFor(_ => _.Description)
                .MinimumLength(0)
                .MaximumLength(200)
                .WithMessage("Açıklama en fazla 200 karakter içerebilir!");

            RuleFor(_ => _.OrderProducts)
                .NotNull()
                .WithMessage("Ürün seçilmedi!");
        }
    }
}
