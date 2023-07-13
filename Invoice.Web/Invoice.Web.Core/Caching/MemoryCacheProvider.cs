using Invoice.Caching;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Invoice.Web.Core.Caching;

public class MemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _cache;

    public MemoryCacheProvider(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrCreateItemAsync<T>(string key, Func<Task<T>> factory)
    {
        var item = await _cache.GetOrCreateAsync(key, async (cacheEntry) =>
        {
            cacheEntry.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(300));

            return JsonConvert.SerializeObject(await factory());
        });

        var data = JsonConvert.DeserializeObject<T>(item);

        if (data == null) throw new ArgumentNullException(nameof(data));

        return data;
    }

    public async Task RemoveItemAsync(string key)
    {
        await Task.Run(() => _cache.Remove(key));
    }
}
