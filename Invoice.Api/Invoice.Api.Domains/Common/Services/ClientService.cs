using Invoice.Api.Domains.Common.Repositories;
using Invoice.Domains.Common.Models;
using Invoice.Validation;

namespace Invoice.Api.Domains.Common.Services;

public interface IClientService
{
    Task<List<ClientModel>> GetAllAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<List<ClientModel>> GetPaginatedAsync(int page, int pageNumber, Expression<Func<ClientModel, bool>>? predicate = null);
    Task<List<ClientModel>> GetTopAsync(int count, Expression<Func<ClientModel, bool>>? predicate = null);
    Task<bool> ExistsAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<int> CountAsync(Expression<Func<ClientModel, bool>>? predicate = null);
    Task<ClientModel?> GetByIdAsync(int id);
    Task<ClientModel> CreateAsync(ClientModel clientModel);
    Task<ClientModel> UpdateAsync(ClientModel clientModel);
    Task DeleteAsync(ClientModel clientModel, bool softDelete = true);
}

public sealed class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientValidatorService _clientValidatorService;
    private readonly IClientAddressValidatorService _clientAddressValidatorService;

    public ClientService(IClientRepository clientRepository, IClientValidatorService clientValidatorService, IClientAddressValidatorService clientAddressValidatorService)
    {
        _clientRepository = clientRepository;
        _clientValidatorService = clientValidatorService;
        _clientAddressValidatorService = clientAddressValidatorService;
    }

    public async Task<int> CountAsync(Expression<Func<ClientModel, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientModel> CreateAsync(ClientModel clientModel)
    {
        await _clientValidatorService.ValidateAsync(clientModel, ValidationMode.Add);

        return await _clientRepository.AddAsync(clientModel);
    }

    public async Task DeleteAsync(ClientModel clientModel, bool softDelete = true)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Expression<Func<ClientModel, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ClientModel>> GetAllAsync(Expression<Func<ClientModel, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ClientModel>> GetPaginatedAsync(int page, int pageNumber, Expression<Func<ClientModel, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ClientModel>> GetTopAsync(int count, Expression<Func<ClientModel, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientModel> UpdateAsync(ClientModel clientModel)
    {
        throw new NotImplementedException();
    }
}
