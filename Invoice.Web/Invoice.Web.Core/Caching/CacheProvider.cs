using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Invoice.Web.Core.Caching;

public interface ICacheProvider
{
    T? GetItem<T>(string key);
    void SetItem(string key, object item);
    void ClearItem(string key);
}

public class MemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheProvider(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void ClearItem(string key)
    {
        _memoryCache.Remove(key);
    }

    public T? GetItem<T>(string key)
    {
        string item;

        if (_memoryCache.TryGetValue(key, out item))
        {
            return JsonConvert.DeserializeObject<T>(item);
        }
        else
        {
            return default;
        }
    }

    public void SetItem(string key, object item)
    {
        _memoryCache.Set(key, JsonConvert.SerializeObject(item), TimeSpan.FromMinutes(15));
    }
}
