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
    private readonly IClientRepository _clientRepository;
    private readonly IClientAddressRepository _clientAddressRepository;

    public ClientAddressValidatorService(IClientRepository clientRepository, IClientAddressRepository clientAddressRepository)
    {
        _clientRepository = clientRepository;
        _clientAddressRepository = clientAddressRepository;
    }

    public async Task ValidateAsync(ClientAddressModel clientAddressModel, ValidationMode validationMode)
    {
        var validator = new ClientAddressValidator(validationMode);

        await validator.ValidateAndThrowAsync(clientAddressModel);

        if (!await _clientRepository.ExistsAsync(e => e.Id == clientAddressModel.ClientId))
        {
            throw new Exception($"No client found for with ClientId '{clientAddressModel.ClientId}'");
        }

        if (await _clientAddressRepository.ExistsAsync(e => e.ClientId == clientAddressModel.ClientId
            && e.Line1 == clientAddressModel.Line1
            && e.Line2 == clientAddressModel.Line2
            && e.Line3 == clientAddressModel.Line3
            && e.Line4 == clientAddressModel.Line4
            && e.City == clientAddressModel.City
            && e.Region == clientAddressModel.Region
            && e.PostalCode == clientAddressModel.PostalCode))
        {
            throw new Exception($"Cannot create duplicate address for ClientId '{clientAddressModel.ClientId}' with Line1 '{clientAddressModel.Line1}'.");
        }
    }
}
