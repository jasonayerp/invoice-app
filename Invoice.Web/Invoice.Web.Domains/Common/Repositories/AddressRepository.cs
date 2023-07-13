using Invoice.Authorization;
using Invoice.Configuration;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Web.Core.Factories;

namespace Invoice.Web.Domains.Common.Repositories;

public interface IAddressRepository
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

public class AddressRepository : IAddressRepository
{
    private readonly IInvoiceHttpClientFactory _factory;
    private readonly ITokenProvider _tokenProvider;
    private readonly IConfigurationReader _configurationReader;

    public AddressRepository(IInvoiceHttpClientFactory factory, ITokenProvider tokenProvider, IConfigurationReader configurationReader)
    {
        _factory = factory;
        _tokenProvider = tokenProvider;
        _configurationReader = configurationReader;
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<AddressModel> CreateAsync(AddressModel address)
    {
        throw new NotImplementedException();
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
        using (HttpClient client = _factory.CreateClient())
        {
            var response = await client.GetAsync("api/v1/addresses/read");

            if (!response.IsSuccessStatusCode)
            {
                var message = $"Http status code: {(int)response.StatusCode} - {response.StatusCode}";
                throw new Exception(message);
            }

            var data = JsonConvert.DeserializeObject<List<AddressObject>>(await response.Content.ReadAsStringAsync());

            return data.Select(Map).ToList();
        }
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

    AddressModel Map(AddressObject address)
    {
        var model = JsonConvert.DeserializeObject<AddressModel>(JsonConvert.SerializeObject(address));
        model.Id = address.Id;
        return model;
    }
}
