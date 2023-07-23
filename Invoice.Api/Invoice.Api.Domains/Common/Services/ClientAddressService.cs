using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Validators;
using Invoice.Services;
using Invoice.Validation;

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
    private readonly IClientAddressRepository _clientAddressRepository;
    private readonly IClientAddressValidatorService _addressValidatorService;
    private readonly IDateTimeService _dateTimeService;

    public ClientAddressService(IClientAddressRepository clientAddressRepository, IDateTimeService dateTimeService, IClientAddressValidatorService addressValidatorService)
    {
        _clientAddressRepository = clientAddressRepository;
        _dateTimeService = dateTimeService;
        _addressValidatorService = addressValidatorService;
    }

    public async Task<ClientAddressModel> CreateAsync(ClientAddressModel address)
    {
        var data = new ClientAddressModel
        {
            Line1 = address.Line1,
            Line2 = address.Line2,
            Line3 = address.Line3,
            Line4 = address.Line4,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            CountryCode = address.CountryCode,
            CreatedAt = _dateTimeService.UtcNow
        };

        await _addressValidatorService.ValidateAsync(address, ValidationMode.Add);

        if (data.IsDefault)
        {
            var defaultAddress = await _clientAddressRepository.GetDefaultByClientIdAsync(address.ClientId);

            defaultAddress.IsDefault = false;
            defaultAddress.UpdatedAt = _dateTimeService.UtcNow;

            await _clientAddressRepository.UpdateAsync(defaultAddress);
        }

        var result = await _clientAddressRepository.AddAsync(data);

        return result;
    }

    public async Task<int> CountAsync()
    {
        return await _clientAddressRepository.CountAsync();
    }

    public async Task<bool> ExistsAsync()
    {
        return await _clientAddressRepository.ExistsAsync();
    }

    public async Task<ClientAddressModel?> GetByIdAsync(int id)
    {
        var data = await _clientAddressRepository.GetByIdAsync(id);

        return data != null ? data : null;
    }

    public async Task DeleteAsync(ClientAddressModel address, bool softDelete = true)
    {
        if (softDelete)
        {
            address.DeletedAt = _dateTimeService.UtcNow;

            await _clientAddressRepository.UpdateAsync(address);
        }
        else
        {
            await _clientAddressRepository.RemoveAsync(address);
        }
    }

    public async Task<List<ClientAddressModel>> GetAllAsync(Expression<Func<ClientAddressModel, bool>>? predicate = null)
    {
        var data = await _clientAddressRepository.ToListAsync(predicate);

        return data.ToList();
    }

    public async Task<List<ClientAddressModel>> GetPaginatedAsync(int page, int pageNumber)
    {
        var data = await _clientAddressRepository.ToListAsync(page, pageNumber);

        return data.ToList();
    }

    public async Task<List<ClientAddressModel>> GetTopAsync(int count)
    {
        var data = await _clientAddressRepository.ToListAsync(count);

        return data.ToList();
    }

    public async Task<ClientAddressModel> UpdateAsync(ClientAddressModel address)
    {
        var data = new ClientAddressModel
        {
            Line1 = address.Line1,
            Line2 = address.Line2,
            Line3 = address.Line3,
            Line4 = address.Line4,
            City = address.City,
            Region = address.Region,
            PostalCode = address.PostalCode,
            CountryCode = address.CountryCode,
            CreatedAt = address.CreatedAt,
            UpdatedAt = _dateTimeService.Now
        };

        var validator = new ClientAddressValidator(ValidationMode.Update);

        validator.ValidateAndThrow(data);

        await _clientAddressRepository.UpdateAsync(data);

        return data;
    }
}
