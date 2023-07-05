using Invoice.Mvc;

namespace Invoice.Api.Mvc;

public class ApiControllerBase : ControllerBase
{
    protected IHttpResult Success()
    {
        return new HttpResult();
    }

    protected IHttpResult Error(string error, string[]? errors = null)
    {
        return new HttpResult(error, errors);
    }

    protected IHttpResult<T> Success<T>(T? data)
    {
        return new HttpResult<T>(data);
    }

    protected IHttpListResult<T> SuccessList<T>(IList<T>? data)
    {
        return new HttpListResult<T>(data);
    }
}
