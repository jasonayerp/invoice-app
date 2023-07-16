using Invoice.Domains.Common.Models;
using Invoice.Web.Domains.Common.Repositories;

namespace Invoice.Web.Domains.Common.Services;

public interface IAddressService
{
    Task<List<AddressModel>> GetAllAsync();
    Task<List<AddressModel>> GetPaginatedAsync(int page, int pageNumber);
    Task<List<AddressModel>> GetTopAsync(int count);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
    Task<AddressModel?> GetByIdAsync(int id);
    Task<AddressModel> CreateAsync(AddressModel address);
    Task<AddressModel> UpdateAsync(AddressModel address);
    Task DeleteAsync(AddressModel address);
}

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AddressModel> CreateAsync(AddressModel address)
    {
        var data = new AddressModel
        {
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            AddressLine3 = address.AddressLine3,
            AddressLine4 = address.AddressLine4,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            CountryCode = address.CountryCode
        };

        return await _addressRepository.CreateAsync(data);
    }

    public async Task DeleteAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<AddressModel>> GetAllAsync()
    {
        return await _addressRepository.GetAllAsync();
    }

    public async Task<AddressModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AddressModel>> GetPaginatedAsync(int page, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AddressModel>> GetTopAsync(int count)
    {
        throw new NotImplementedException();
    }

    public async Task<AddressModel> UpdateAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }
}