using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TekhneCafe.Api.Consts;
using TekhneCafe.Business.ValidationRules.FluentValidations.Order;
using TekhneCafe.Core.Exceptions;

namespace TekhneCafe.Api.ActionFilters
{
    public class ValidationFilterAttribute<TValidator, T> : IActionFilter
        where TValidator : IValidator<T>, new()
        where T : class, new()
    {
        private readonly ValidationType _validationType;

        public ValidationFilterAttribute(ValidationType validationType)
        {
            _validationType = validationType;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count == 0)
                return;
            var argument = context.ActionArguments.Values.FirstOrDefault();
            if (_validationType == ValidationType.FluentValidation && FluentValidation(context, argument))
                return;
            else if (_validationType == ValidationType.ModelValidation && ModelValidation(context))
                return;
            throw new BadRequestException(GetFirstModelError(context));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private bool FluentValidation(ActionExecutingContext context, object argument)
        {
            TValidator validator = new TValidator();
            var validationResult = validator.Validate((T)argument);
            if (validationResult.Errors.Count == 0)
                return true;
            if (!validationResult.IsValid)
                foreach (var error in validationResult.Errors)
                    context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            return false;
        }

        private bool ModelValidation(ActionExecutingContext context)
            => context.ModelState.IsValid;

        private string GetFirstModelError(ActionExecutingContext context)
            => context.ModelState.Values.First().Errors.First().ErrorMessage;
    }
}
