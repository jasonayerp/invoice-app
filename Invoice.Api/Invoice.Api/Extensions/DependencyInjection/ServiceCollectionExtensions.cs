using Invoice.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Reflection;

namespace Invoice.Api.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddConfigurationReader(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationReader, NetCoreConfigurationReader>();
    }

    public static void AddDomain(this IServiceCollection services, string domainNamespace)
    {
        Assembly assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetReferencedAssemblies().First(e => e.FullName.Contains("Invoice.Api.Domains")));

        Type[] implementationTypes = assembly.GetTypes().Where(e => e.FullName.StartsWith($"Invoice.Api.Domains.{domainNamespace}.Services") || e.FullName.StartsWith($"Invoice.Api.Domains.{domainNamespace}.Repositories")).ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type[] interfaces = ((TypeInfo)implementationType).GetInterfaces();
            Type[] serviceTypes = interfaces as Type[] ?? interfaces.ToArray();
            if (serviceTypes.Count() < 1) continue;
            if (serviceTypes.Count() > 1) throw new InvalidOperationException($"More than one implementations found for ServiceType '{implementationType.Name}'.");
            Type serviceType = serviceTypes.First();
            services.TryAddScoped(serviceType, implementationType);
        }
    }
}
