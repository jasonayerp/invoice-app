using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Validators;
using Invoice.Services;
using Invoice.Validation;
using System.Reflection;

namespace Invoice.Api.Domains.Common.Services;

public interface IClientAddressService
{
    Task<List<ClientAddressModel>> GetAllAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null);
    Task<List<ClientAddressModel>> GetPaginatedAsync(int page, int pageNumber);
    Task<List<ClientAddressModel>> GetTopAsync(int count);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
    Task<ClientAddressModel?> GetByIdAsync(int id);
    Task<ClientAddressModel> CreateAsync(ClientAddressModel address);
    Task<ClientAddressModel> UpdateAsync(ClientAddressModel address);
    Task DeleteAsync(ClientAddressModel address, bool softDelete = true);
}

internal sealed class ClientAddressService : IClientAddressService
{
    private readonly IClientAddressRepository _addressRepository;
    private readonly IDateTimeService _dateTimeService;

    public ClientAddressService(IClientAddressRepository addressRepository, IDateTimeService dateTimeService)
    {
        _addressRepository = addressRepository;
        _dateTimeService = dateTimeService;
    }

    public async Task<ClientAddressModel> CreateAsync(ClientAddressModel address)
    {
        var data = new ClientAddressModel
        {
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            AddressLine3 = address.AddressLine3,
            AddressLine4 = address.AddressLine4,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            CountryCode = address.CountryCode,
            CreatedAt = _dateTimeService.Now
        };

        var validator = new ClientAddressValidator(ValidationMode.Add);

        validator.ValidateAndThrow(data);

        var result = await _addressRepository.AddAsync(data);

        return Configure(result);
    }

    public async Task<int> CountAsync()
    {
        return await _addressRepository.CountAsync();
    }

    public async Task<bool> ExistsAsync()
    {
        return await _addressRepository.ExistsAsync();
    }

    public async Task<ClientAddressModel?> GetByIdAsync(int id)
    {
        var data = await _addressRepository.GetByIdAsync(id);

        return data != null ? Configure(data) : null;
    }

    public async Task DeleteAsync(ClientAddressModel address, bool softDelete = true)
    {
        if (softDelete)
        {
            address.DeletedAt = _dateTimeService.Now;

            await _addressRepository.UpdateAsync(address);
        }
        else
        {
            await _addressRepository.RemoveAsync(address);
        }
    }

    public async Task<List<ClientAddressModel>> GetAllAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null)
    {
        var data = await _addressRepository.ToListAsync(predicate);

        return data.Select(Configure).ToList();
    }

    public async Task<List<ClientAddressModel>> GetPaginatedAsync(int page, int pageNumber)
    {
        var data = await _addressRepository.ToListAsync(page, pageNumber);

        return data.Select(Configure).ToList();
    }

    public async Task<List<ClientAddressModel>> GetTopAsync(int count)
    {
        var data = await _addressRepository.ToListAsync(count);

        return data.Select(Configure).ToList();
    }

    public async Task<ClientAddressModel> UpdateAsync(ClientAddressModel address)
    {
        var data = new ClientAddressModel
        {
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            AddressLine3 = address.AddressLine3,
            AddressLine4 = address.AddressLine4,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            CountryCode = address.CountryCode,
            CreatedAt = address.CreatedAt,
            UpdatedAt = _dateTimeService.Now
        };

        var validator = new ClientAddressValidator(ValidationMode.Update);

        validator.ValidateAndThrow(data);

        await _addressRepository.UpdateAsync(data);

        return Configure(data);
    }

    private ClientAddressModel Configure(ClientAddressModel obj)
    {
        Type typeFromHandle = typeof(ClientAddressModel);
        PropertyInfo[] properties = typeFromHandle.GetProperties();
        foreach (PropertyInfo propertyInfo in properties)
        {
            if (propertyInfo.PropertyType == typeof(DateTime))
            {
                DateTime value = (DateTime)propertyInfo.GetValue(obj, null);
                value = DateTime.SpecifyKind(value, DateTimeKind.);
                propertyInfo.SetValue(obj, value, null);
            }
            else if (propertyInfo.PropertyType == typeof(DateTime?))
            {
                DateTime? dateTime = (DateTime?)propertyInfo.GetValue(obj, null);
                if (dateTime.HasValue)
                {
                    propertyInfo.SetValue(value: new DateTime?(DateTime.SpecifyKind(dateTime.Value, DateTimeKind.)), obj: obj, index: null);
                }
            }
        }

        return obj;
    }
}
