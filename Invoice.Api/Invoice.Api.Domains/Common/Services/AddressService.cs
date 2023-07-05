using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Validators;
using Invoice.System;

namespace Invoice.Api.Domains.Common.Services;

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
    Task SoftDeleteAsync(AddressModel address);
}

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;
    private readonly IDateTimeService _dateTimeService;

    public AddressService(IAddressRepository addressRepository, IDateTimeService dateTimeService)
    {
        _addressRepository = addressRepository;
        _dateTimeService = dateTimeService;
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
            CountryCode = address.CountryCode,
            UtcCreatedDate = _dateTimeService.UtcNow
        };

        var validator = new AddressValidator(Validation.ValidationMode.Add);

        validator.ValidateAndThrow(data);

        return await _addressRepository.AddAsync(data);
    }

    public async Task<int> CountAsync()
    {
        return await _addressRepository.CountAsync();
    }

    public async Task<bool> ExistsAsync()
    {
        return await _addressRepository.ExistsAsync();
    }

    public async Task<AddressModel?> GetByIdAsync(int id)
    {
        return await _addressRepository.GetByIdAsync(id);
    }

    public async Task DeleteAsync(AddressModel address)
    {
        await _addressRepository.RemoveAsync(address);
    }

    public async Task SoftDeleteAsync(AddressModel address)
    {
        await _addressRepository.RemoveAsync(address);
    }

    public async Task<List<AddressModel>> GetAllAsync()
    {
        return await _addressRepository.ToListAsync();
    }

    public async Task<List<AddressModel>> GetPaginatedAsync(int page, int pageNumber)
    {
        return await _addressRepository.ToListAsync(page, pageNumber);
    }

    public async Task<List<AddressModel>> GetTopAsync(int count)
    {
        return await _addressRepository.ToListAsync(count);
    }

    public async Task<AddressModel> UpdateAsync(AddressModel address)
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
            CountryCode = address.CountryCode,
            UtcCreatedDate = address.UtcCreatedDate,
            UtcUpdatedDate = _dateTimeService.UtcNow
        };

        var validator = new AddressValidator(Validation.ValidationMode.Update);

        validator.ValidateAndThrow(data);

        await _addressRepository.UpdateAsync(data);

        return data;
    }
}
