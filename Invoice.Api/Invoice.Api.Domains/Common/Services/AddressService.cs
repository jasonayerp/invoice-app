using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Validators;
using Invoice.Services;
using System.Reflection;

namespace Invoice.Api.Domains.Common.Services;

public interface IAddressService
{
    Task<List<AddressModel>> GetAllAsync(Expression<Func<AddressModel, bool>>? predicate = null);
    Task<List<AddressModel>> GetPaginatedAsync(int page, int pageNumber);
    Task<List<AddressModel>> GetTopAsync(int count);
    Task<bool> ExistsAsync();
    Task<int> CountAsync();
    Task<AddressModel?> GetByIdAsync(int id);
    Task<AddressModel> CreateAsync(AddressModel address);
    Task<AddressModel> UpdateAsync(AddressModel address);
    Task DeleteAsync(AddressModel address, bool softDelete = true);
}

internal sealed class AddressService : IAddressService
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

    public async Task<AddressModel?> GetByIdAsync(int id)
    {
        var data = await _addressRepository.GetByIdAsync(id);

        return data != null ? Configure(data) : null;
    }

    public async Task DeleteAsync(AddressModel address, bool softDelete = true)
    {
        if (softDelete)
        {
            address.UtcDeletedDate = _dateTimeService.UtcNow;

            await _addressRepository.UpdateAsync(address);
        }
        else
        {
            await _addressRepository.RemoveAsync(address);
        }
    }

    public async Task<List<AddressModel>> GetAllAsync(Expression<Func<AddressModel, bool>>? predicate = null)
    {
        var data = await _addressRepository.ToListAsync(predicate);

        return data.Select(Configure).ToList();
    }

    public async Task<List<AddressModel>> GetPaginatedAsync(int page, int pageNumber)
    {
        var data = await _addressRepository.ToListAsync(page, pageNumber);

        return data.Select(Configure).ToList();
    }

    public async Task<List<AddressModel>> GetTopAsync(int count)
    {
        var data = await _addressRepository.ToListAsync(count);

        return data.Select(Configure).ToList();
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

        return Configure(data);
    }

    private AddressModel Configure(AddressModel obj)
    {
        Type typeFromHandle = typeof(AddressModel);
        PropertyInfo[] properties = typeFromHandle.GetProperties();
        foreach (PropertyInfo propertyInfo in properties)
        {
            if (propertyInfo.PropertyType == typeof(DateTime))
            {
                DateTime value = (DateTime)propertyInfo.GetValue(obj, null);
                value = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                propertyInfo.SetValue(obj, value, null);
            }
            else if (propertyInfo.PropertyType == typeof(DateTime?))
            {
                DateTime? dateTime = (DateTime?)propertyInfo.GetValue(obj, null);
                if (dateTime.HasValue)
                {
                    propertyInfo.SetValue(value: new DateTime?(DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc)), obj: obj, index: null);
                }
            }
        }

        return obj;
    }
}
