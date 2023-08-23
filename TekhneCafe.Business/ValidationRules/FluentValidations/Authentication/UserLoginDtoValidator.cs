using FluentValidation;
using TekhneCafe.Core.DTOs.Authentication;

namespace TekhneCafe.Business.ValidationRules.FluentValidations.Authentication
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(_ => _.Username)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithMessage("Must containe at least 5 at most 100 charachters!");

            RuleFor(_ => _.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .WithMessage("Password is required!");
        }
    }
}
