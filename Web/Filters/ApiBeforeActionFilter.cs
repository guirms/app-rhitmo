using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class ApiBeforeActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}