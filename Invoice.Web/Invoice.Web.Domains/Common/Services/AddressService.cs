using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mvc;
using Invoice.Web.Core.Caching;
using Invoice.Web.Domains.Common.Repositories;

namespace Invoice.Web.Domains.Common.Services;

public interface IAddressService
{
    Task<IHttpCollectionResult<AddressObject>> GetAllAsync();
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

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<IHttpResult<int>> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<AddressObject>> CreateAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult> DeleteAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<bool>> ExistsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpCollectionResult<AddressObject>> GetAllAsync()
    {
        return await _addressRepository.GetAllAsync();
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