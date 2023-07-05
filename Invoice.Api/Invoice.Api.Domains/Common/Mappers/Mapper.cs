using System.Text.Json;

namespace Invoice.Api.Domains.Common.Mappers;

public interface IMapper
{
    T Map<T>(object value);
}


public class JsonMapper : IMapper
{
    public T Map<T>(object value)
    {
        byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(value);

        return JsonSerializer.Deserialize<T>(jsonUtf8Bytes);
    }
}
