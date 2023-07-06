namespace Invoice.Mvc;

public interface IHttpResult
{
    bool IsSuccess { get; }
    Error? Error { get; }
}

public interface IHttpResult<T> : IHttpResult
{
    T? Data { get; }
}

public interface IHttpCollectionResult<T> : IHttpResult
{
    ICollection<T>? Data { get; }
}

public class HttpResult : IHttpResult
{
    public bool IsSuccess { get; private set; } = true;
    public Error? Error { get; private set; }

    public HttpResult()
    {
    }

    public HttpResult(Error error)
        : base()
    {
        IsSuccess = false;
        Error = error;
    }
}

public class HttpResult<T> : HttpResult, IHttpResult<T>
{
    public T? Data { get; private set; }

    public HttpResult(T? data)
        : base()
    {
        Data = data;
    }

    public HttpResult(Error error)
        : base(error)
    {
    }
}

public class HttpCollectionResult<T> : HttpResult, IHttpCollectionResult<T>
{
    public ICollection<T>? Data { get; private set; }

    public HttpCollectionResult(ICollection<T>? data)
        : base()
    {
        Data = data;
    }

    public HttpCollectionResult(Error error)
        : base(error)
    {
    }
}