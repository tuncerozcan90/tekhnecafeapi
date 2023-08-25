using Microsoft.AspNetCore.Mvc.Filters;
using TekhneCafe.Core.Exceptions;

namespace TekhneCafe.Api.ActionFilters
{
    public class ModelValidationFilterAttribute : IActionFilter
    {
        private readonly string _key;
        public ModelValidationFilterAttribute(string key)
        {
            _key = key;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            ModelValidation(context);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private void ModelValidation(ActionExecutingContext context)
        {
            var parameter = context.ModelState.FirstOrDefault(_ => _.Key == _key);
            if (parameter.Value.ValidationState != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
                throw new BadRequestException(parameter.Value.Errors.First().ErrorMessage);
        }
    }
}
