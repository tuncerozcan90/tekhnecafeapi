using Microsoft.AspNetCore.Mvc.Filters;
using TekhneCafe.Core.Exceptions;

namespace TekhneCafe.Core.ActionFilters
{
    public class ValidateModelState : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;
            //context.ActionArguments.SingleOrDefault()
            throw new BadRequestException("Invalid model!");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
