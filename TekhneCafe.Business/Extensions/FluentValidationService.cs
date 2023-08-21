using FluentValidation.Results;
using TekhneCafe.Core.Exceptions;

namespace TekhneCafe.Business.Extensions
{
    public static class FluentValidationService
    {
        public static void ThrowExceptionIfNotValid(this ValidationResult result, string errorMessage = null)
        {
            if (result.IsValid)
                return;

            throw new BadRequestException(errorMessage is null ? result.Errors[0].ErrorMessage : errorMessage);
        }
    }
}
