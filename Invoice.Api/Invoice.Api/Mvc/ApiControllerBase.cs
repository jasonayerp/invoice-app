using Invoice.Mvc;

namespace Invoice.Api.Mvc;

public class ApiControllerBase : ControllerBase
{
    protected IHttpResult Success()
    {
        return new HttpResult();
    }

    protected IHttpResult Error(Error error)
    {
        return new HttpResult(error);
    }

    protected IHttpResult<T> Success<T>(T? data)
    {
        return new HttpResult<T>(data);
    }

    protected IHttpCollectionResult<T> SuccessList<T>(ICollection<T>? data)
    {
        return new HttpCollectionResult<T>(data);
    }
}
