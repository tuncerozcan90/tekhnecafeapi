using FluentValidation;
using TekhneCafe.Core.DTOs.OrderProductAttribute;

namespace TekhneCafe.Business.ValidationRules.FluentValidations.OrderProductAttribute
{
    public class OrderProductAttributeAddDtoValidator : AbstractValidator<OrderProductAttributeAddDto>
    {
        public OrderProductAttributeAddDtoValidator()
        {
            RuleFor(_ => _.ProductAttributeId)
                .NotNull()
                .WithMessage("Ürün id'si boş bırakılamaz!");

            RuleFor(_ => _.Quantity)
              .GreaterThan(0)
              .LessThan(100)
              .WithMessage("En fazla 100 adet seçilebilir!");
        }
    }
}
