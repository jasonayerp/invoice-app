using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Validators;
using Invoice.Validation;

namespace Invoice.Api.Domains.Common.Services;

public interface IClientAddressValidatorService
{
    Task ValidateAsync(ClientAddressModel clientAddressModel, ValidationMode validationMode);
}

public sealed class ClientAddressValidatorService : IClientAddressValidatorService
{
    private readonly IClientAddressRepository _clientAddressRepository;

    public ClientAddressValidatorService(IClientAddressRepository clientAddressRepository)
    {
        _clientAddressRepository = clientAddressRepository;
    }

    public async Task ValidateAsync(ClientAddressModel clientAddressModel, ValidationMode validationMode)
    {
        var validator = new ClientAddressValidator(validationMode);

        await validator.ValidateAndThrowAsync(clientAddressModel);

        if (await _clientAddressRepository.ExistsAsync(e => e.ClientId == clientAddressModel.ClientId
            && e.AddressLine1 == clientAddressModel.AddressLine1
            && e.AddressLine2 == clientAddressModel.AddressLine2
            && e.AddressLine3 == clientAddressModel.AddressLine3
            && e.AddressLine4 == clientAddressModel.AddressLine4
            && e.City == clientAddressModel.City
            && e.Region == clientAddressModel.Region
            && e.PostalCode == clientAddressModel.PostalCode))
        {
            throw new Exception($"Cannot create duplicate address for ClientId '{clientAddressModel.ClientId}' with AddressLine1 '{clientAddressModel.AddressLine1}'.");
        }
    }
}
