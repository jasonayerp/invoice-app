using Invoice.Authorization;
using Invoice.Configuration;
using Invoice.Domains.Common.Models;
using Invoice.Domains.Common.Objects;
using Invoice.Mvc;
using Invoice.Web.Core;

namespace Invoice.Web.Domains.Common.Repositories;

public interface IAddressRepository
{
    Task<List<AddressObject>> GetAllAsync();
    Task<IHttpCollectionResult<AddressObject>> GetPaginatedAsync(int page, int pageNumber);
    Task<IHttpCollectionResult<AddressObject>> GetTopAsync(int count);
    Task<IHttpResult<bool>> ExistsAsync();
    Task<IHttpResult<int>> CountAsync();
    Task<IHttpResult<AddressObject?>> GetByIdAsync(int id);
    Task<IHttpResult<AddressObject>> CreateAsync(AddressModel address);
    Task<IHttpResult<AddressObject>> UpdateAsync(AddressModel address);
    Task<IHttpResult> DeleteAsync(AddressModel address);
    Task<IHttpResult> SoftDeleteAsync(AddressModel address);
}

public class AddressRepository : IAddressRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;
    private readonly IConfigurationReader _configurationReader;

    public AddressRepository(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider, IConfigurationReader configurationReader)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
        _configurationReader = configurationReader;
    }

    public async Task<IHttpResult<int>> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<AddressObject>> CreateAsync(AddressModel address)
    {
        using (HttpClient client = _httpClientFactory.CreateClient())
        {
            var token = await _tokenProvider.GetTokenAsync();

            string? baseUri = _configurationReader.GetValue("ApiBaseUri");

            if (string.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("ApiBaseUri cannot be null or empty.");

            client.BaseAddress = new Uri(baseUri.TrimEnd('/'));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/addresses/create");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);

            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

            return JsonConvert.DeserializeObject<IHttpResult<AddressObject>>(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task<IHttpResult> DeleteAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<bool>> ExistsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<AddressObject>> GetAllAsync()
    {
        using (HttpClient client = _httpClientFactory.CreateClient())
        {
            var token = await _tokenProvider.GetTokenAsync();

            string? baseUri = _configurationReader.GetValue("ApiBaseUri");

            if (string.IsNullOrEmpty(baseUri)) throw new ArgumentNullException("ApiBaseUri cannot be null or empty.");

            client.BaseAddress = new Uri(baseUri.TrimEnd('/'));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/addresses/read");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"Http status code: {(int)response.StatusCode} - {response.StatusCode}";
                throw new Exception(message);
            }

            var data = JsonConvert.DeserializeObject<List<AddressObject>>(await response.Content.ReadAsStringAsync());

            return data;
        }
    }

    public async Task<IHttpResult<AddressObject?>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpCollectionResult<AddressObject>> GetPaginatedAsync(int page, int pageNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpCollectionResult<AddressObject>> GetTopAsync(int count)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult> SoftDeleteAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }

    public async Task<IHttpResult<AddressObject>> UpdateAsync(AddressModel address)
    {
        throw new NotImplementedException();
    }
}
