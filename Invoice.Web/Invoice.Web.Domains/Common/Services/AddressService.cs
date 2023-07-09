using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mvc;
using Invoice.Web.Domains.Common.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.InteropServices;

namespace Invoice.Web.Domains.Common.Services;

public interface IAddressService
{
    Task<List<AddressObject>> GetAllAsync();
    Task<IHttpCollectionResult<AddressObject>> GetPaginatedAsync(int page, int pageNumber);
    Task<IHttpCollectionResult<AddressObject>> GetTopAsync(int count);
    Task<IHttpResult<bool>> ExistsAsync();
    Task<IHttpResult<int>> CountAsync();
    Task<IHttpResult<AddressObject?>> GetByIdAsync(int id);
    Task<IHttpResult<AddressObject>> CreateAsync(AddressModel address);
    Task<IHttpResult<AddressObject>> UpdateAsync(AddressModel address);
    Task<IHttpResult> DeleteAsync(AddressModel address);
    Task<IHttpResult> SoftDeleteAsync(AddressModel address);
}

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IMemoryCache _memoryCache;

    public AddressService(IAddressRepository addressRepository, IMemoryCache memoryCache)
    {
        _addressRepository = addressRepository;
        _memoryCache = memoryCache;
    }

    public async Task<IHttpResult<int>> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<AddressObject>> CreateAsync(AddressModel address)
    {
        _memoryCache.Remove("invoices");

        return await _addressRepository.CreateAsync(address);
    }

    public async Task<IHttpResult> DeleteAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<bool>> ExistsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<AddressObject>> GetAllAsync()
    {
        var item = await _memoryCache.GetOrCreateAsync("invoices", async (cacheEntry) =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(300);

            var data = await _addressRepository.GetAllAsync();

            return JsonConvert.SerializeObject(data);
        });

        return JsonConvert.DeserializeObject<List<AddressObject>>(item);
    }

    public async Task<IHttpResult<AddressObject?>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpCollectionResult<AddressObject>> GetPaginatedAsync(int page, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpCollectionResult<AddressObject>> GetTopAsync(int count)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult> SoftDeleteAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<AddressObject>> UpdateAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }
}