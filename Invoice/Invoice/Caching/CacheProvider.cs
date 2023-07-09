using Microsoft.Extensions.Caching.Memory;

namespace Invoice.Caching;

public interface ICacheProvider
{
    Task<T> GetOrCreateItemAsync<T>(string key, Func<Task<T>> factory);
    Task RemoveItemAsync(string key);
}
