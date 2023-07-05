

using Invoice.Configuration;

namespace Invoice.Api.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddConfigurationReader(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationReader, NetCoreConfigurationReader>();
    }
}
