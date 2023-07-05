namespace Invoice.Mvc;

public interface IHttpResult
{
    bool IsSuccess { get; }
    string Error { get; }
    string[]? Errors { get; }
}

public interface IHttpResult<T> : IHttpResult
{
    T? Data { get; }
}

public interface IHttpListResult<T> : IHttpResult
{
    IList<T>? Data { get; }
}

public class HttpResult : IHttpResult
{
    public bool IsSuccess { get; private set; } = true;
    public string Error { get; private set; } = "Ok";
    public string[]? Errors { get; private set; }

    public HttpResult()
    {
    }

    public HttpResult(string error, string[]? errors = null)
        : base()
    {
        IsSuccess = false;
        Error = error;
        Errors = errors;
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

    public HttpResult(string error, string[]? errors = null)
        : base(error, errors)
    {
    }
}

public class HttpListResult<T> : HttpResult, IHttpListResult<T>
{
    public IList<T>? Data { get; private set; }

    public HttpListResult(IList<T>? data)
        : base()
    {
        Data = data;
    }

    public HttpListResult(string error, string[]? errors = null)
        : base(error, errors)
    {
    }
}