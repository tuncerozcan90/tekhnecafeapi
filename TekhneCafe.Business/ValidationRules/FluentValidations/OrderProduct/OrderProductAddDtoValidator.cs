using FluentValidation;
using TekhneCafe.Core.DTOs.OrderProduct;

namespace TekhneCafe.Business.ValidationRules.FluentValidations.OrderProduct
{
    public class OrderProductAddDtoValidator : AbstractValidator<OrderProductAddDto>
    {
        public OrderProductAddDtoValidator()
        {
            RuleFor(_ => _.ProductId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ürün id'si boş bırakılamaz!");

            RuleFor(_ => _.Quantity)
                .GreaterThan(0)
                .LessThan(100)
                .WithMessage("En fazla 100 adet seçilebilir!");
        }
    }
}
