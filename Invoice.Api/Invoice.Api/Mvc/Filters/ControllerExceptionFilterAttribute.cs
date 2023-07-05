using Microsoft.AspNetCore.Mvc.Filters;

namespace Invoice.Api.Mvc.Filters;

public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        
    }
}
