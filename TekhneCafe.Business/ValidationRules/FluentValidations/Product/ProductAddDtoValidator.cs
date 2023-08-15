using FluentValidation;
using TekhneCafe.Core.DTOs.Product;

namespace TekhneCafe.Business.ValidationRules.FluentValidations.Product
{
    public class ProductAddDtoValidator : AbstractValidator<ProductAddDto>
    {
        public ProductAddDtoValidator()
        {
            RuleFor(_ => _.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name must contain a value !")
                .MinimumLength(2)
                .WithMessage("Name must contain at least 2 charachters !")
                .MaximumLength(50)
                .WithMessage("Name must contain at most 50 charachters !");

            RuleFor(_ => _.Description)
                .MaximumLength(150)
                .WithMessage("Description must contain at most 150 charachters !");

            RuleFor(_ => _.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price must contain a value !")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greate or equal to 0 !")
                .LessThan(1000)
                .WithMessage("Price must be less than 1000 !");
        }
    }
}
