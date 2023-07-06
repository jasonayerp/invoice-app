using Invoice.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Invoice.Api.Mvc.Filters;

public class ControllerExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var error = new Error
        {
            Errors = new List<ErrorDetail>
            {
                new ErrorDetail
                {
                    Reason = exception.GetType().Name,
                    Message = exception.Message
                }
            }
        };

        context.Result = new ObjectResult(new HttpResult(error));
    }
}
