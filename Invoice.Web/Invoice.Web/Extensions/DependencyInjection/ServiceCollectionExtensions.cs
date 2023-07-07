using Invoice.Authentication;
using Invoice.Configuration;
using Invoice.Web.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Invoice.Web.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddTokenProvider(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, Auth0TokenProvider>();
    }

    public static void AddConfigurationReader(this IServiceCollection services)
    {
        services.AddScoped<IConfigurationReader, NetCoreConfigurationReader>();
    }

    public static void AddScopedDomain(this IServiceCollection services, string domainNamespace)
    {
        Assembly assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetReferencedAssemblies().First(e => e.FullName.Contains("Invoice.Web.Domains")));

        Type[] implementationTypes = assembly.GetTypes().Where(e => e.FullName.StartsWith($"Invoice.Web.Domains.{domainNamespace}.Services") || e.FullName.StartsWith($"Invoice.Web.Domains.{domainNamespace}.Repositories")).ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type[] interfaces = ((TypeInfo)implementationType).GetInterfaces();
            Type[] serviceTypes = interfaces as Type[] ?? interfaces.ToArray();
            if (serviceTypes.Count() < 1) continue;
            if (serviceTypes.Count() > 1) throw new InvalidOperationException($"More than one implementations found for ServiceType '{implementationType.Name}'.");
            Type serviceType = serviceTypes.First();
            AddDomain(services, ServiceLifetime.Transient, serviceType, implementationType);
        }
    }

    public static void AddTransientdDomain(this IServiceCollection services, string domainNamespace)
    {
        Assembly assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetReferencedAssemblies().First(e => e.FullName.Contains("Invoice.Web.Domains")));

        Type[] implementationTypes = assembly.GetTypes().Where(e => e.FullName.StartsWith($"Invoice.Web.Domains.{domainNamespace}.Services") || e.FullName.StartsWith($"Invoice.Web.Domains.{domainNamespace}.Repositories")).ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type[] interfaces = ((TypeInfo)implementationType).GetInterfaces();
            Type[] serviceTypes = interfaces as Type[] ?? interfaces.ToArray();
            if (serviceTypes.Count() < 1) continue;
            if (serviceTypes.Count() > 1) throw new InvalidOperationException($"More than one implementations found for ServiceType '{implementationType.Name}'.");
            Type serviceType = serviceTypes.First();
            AddDomain(services, ServiceLifetime.Scoped, serviceType, implementationType);
        }
    }

    public static void AddSingletonDomain(this IServiceCollection services, string domainNamespace)
    {
        Assembly assembly = Assembly.Load(Assembly.GetExecutingAssembly().GetReferencedAssemblies().First(e => e.FullName.Contains("Invoice.Web.Domains")));

        Type[] implementationTypes = assembly.GetTypes().Where(e => e.FullName.StartsWith($"Invoice.Web.Domains.{domainNamespace}.Services") || e.FullName.StartsWith($"Invoice.Web.Domains.{domainNamespace}.Repositories")).ToArray();

        foreach (Type implementationType in implementationTypes)
        {
            Type[] interfaces = ((TypeInfo)implementationType).GetInterfaces();
            Type[] serviceTypes = interfaces as Type[] ?? interfaces.ToArray();
            if (serviceTypes.Count() < 1) continue;
            if (serviceTypes.Count() > 1) throw new InvalidOperationException($"More than one implementations found for ServiceType '{implementationType.Name}'.");
            Type serviceType = serviceTypes.First();
            AddDomain(services, ServiceLifetime.Singleton, serviceType, implementationType);
        }
    }

    private static void AddDomain(IServiceCollection services, ServiceLifetime serviceLifetime, Type serviceType, Type implementationType)
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Transient:
                services.TryAddTransient(serviceType, implementationType);
                break;
            case ServiceLifetime.Scoped:
                services.TryAddScoped(serviceType, implementationType);
                break;
            case ServiceLifetime.Singleton:
                services.TryAddSingleton(serviceType, implementationType);
                break;
            default:
                services.TryAddSingleton(serviceType, implementationType);
                break;
        }
    }
}
