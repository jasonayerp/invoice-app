using Microsoft.Extensions.Configuration;

namespace Invoice.Configuration;

public interface IConfigurationReader
{
    string? GetValue(string key);
    T? GetValue<T>(string key, T? defaultValue);
}

public class NetCoreConfigurationReader : IConfigurationReader
{
    private readonly IConfiguration _configuration;

    public NetCoreConfigurationReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string? GetValue(string key)
    {
        var primaryValue = string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)) ? null : Environment.GetEnvironmentVariable(key);
        var secondaryValue = _configuration.GetValue<string>($"Configuration:{key}");

        return primaryValue ?? secondaryValue;
    }

    public T? GetValue<T>(string key, T? defaultValue)
    {
        var primaryValue = Environment.GetEnvironmentVariable(key);
        var secondaryValue = _configuration.GetValue($"Configuration:{key}", defaultValue);
        
        return string.IsNullOrEmpty(primaryValue) ? (T?)(object?)primaryValue : secondaryValue;
    }
}
