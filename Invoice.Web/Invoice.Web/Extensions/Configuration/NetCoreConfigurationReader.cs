using Invoice.Configuration;

namespace Invoice.Web.Extensions.Configuration;

public class NetCoreConfigurationReader : IConfigurationReader
{
    private readonly IConfiguration _configuration;

    public NetCoreConfigurationReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string? GetValue(string key)
    {
        var value = Environment.GetEnvironmentVariable(key);

        if (value == null)
        {
            value = _configuration.GetValue<string>($"Configuration:{key}");
        }

        return value;
    }

    public T? GetValue<T>(string key, T? defaultValue)
    {
        var value = GetValue(key);

        return value != null ? (T)Convert.ChangeType(value, typeof(T)) : defaultValue;
    }
}
