using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Validators;
using Invoice.Validation;

namespace Invoice.Api.Domains.Common.Services;

public interface IClientValidatorService
{
    Task ValidateAsync(ClientModel clientModel, ValidationMode validationMode);
}

public sealed class ClientValidatorService : IClientValidatorService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientAddressValidatorService _clientAddressValidatorService;

    public ClientValidatorService(IClientRepository clientRepository, IClientAddressValidatorService clientAddressValidatorService)
    {
        _clientRepository = clientRepository;
        _clientAddressValidatorService = clientAddressValidatorService;
    }

    public async Task ValidateAsync(ClientModel clientModel, ValidationMode validationMode)
    {
        var validator = new ClientValidator(validationMode);

        await validator.ValidateAndThrowAsync(clientModel);

        if (await _clientRepository.ExistsAsync(e => e.Name == clientModel.Name))
        {
            throw new Exception($"Cannot create duplicate client with Name '{clientModel.Name}'");
        }

        foreach (var clientAddress in clientModel.Addresses)
        {
            await _clientAddressValidatorService.ValidateAsync(clientAddress, validationMode);
        }
    }
}
