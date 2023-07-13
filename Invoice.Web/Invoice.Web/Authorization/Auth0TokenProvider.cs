using Invoice.Auth0;
using Invoice.Authorization;
using Invoice.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Invoice.Web.Authorization;

public class Auth0TokenProvider : ITokenProvider
{
    private readonly IMemoryCache _memoryCache;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfigurationReader _configurationReader;

    public Auth0TokenProvider(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory, IConfigurationReader configurationReader)
    {
        _memoryCache = memoryCache;
        _httpClientFactory = httpClientFactory;
        _configurationReader = configurationReader;
    }

    public async Task<Token> GetTokenAsync()
    {
        _memoryCache.Remove("token");

        var cacheItem = await _memoryCache.GetOrCreateAsync("token", async (cacheOptions) =>
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                string? endpoint = _configurationReader.GetValue("Auth0Endpoint");
                string? clientId = _configurationReader.GetValue("Auth0ClientId");
                string? clientSecret = _configurationReader.GetValue("Auth0ClientSecret");
                string? audience = _configurationReader.GetValue("Auth0Audience");
                string? grantType = _configurationReader.GetValue("Auth0GrantType");

                if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException("Endpoint cannot be null or empty.");
                if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("ClientId cannot be null or empty.");
                if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentNullException("ClientSecret cannot be null or empty.");
                if (string.IsNullOrEmpty(audience)) throw new ArgumentNullException("Audience cannot be null or empty.");
                if (string.IsNullOrEmpty(grantType)) throw new ArgumentNullException("GrantType cannot be null or empty.");

                var bodyContent = new Auth0TokenRequest { ClientId = clientId, ClientSecret = clientSecret, Audience = audience, GrantType = grantType };

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                request.Content = new StringContent(JsonConvert.SerializeObject(bodyContent), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

                Auth0TokenResponse? result = JsonConvert.DeserializeObject<Auth0TokenResponse>(await response.Content.ReadAsStringAsync());

                if (result == null) throw new ArgumentNullException("JsonWebToken cannot be null.");

                Token token = new Token
                {
                    AccessToken = result.AccessToken,
                    Scope = result.Scope,
                    TokenType = result.TokenType,
                    ExpiresIn = result.ExpiresIn
                };

                cacheOptions.AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn - 300);

                return JsonConvert.SerializeObject(token);
            }
        });

        return JsonConvert.DeserializeObject<Token>(cacheItem);
    }
}

[JsonObject]
public class Auth0TokenRequest
{
    [JsonProperty("client_id")]
    public string ClientId { get; set; } = "";
    [JsonProperty("client_secret")]
    public string ClientSecret { get; set; } = "";
    [JsonProperty("audience")]
    public string Audience { get; set; } = "";
    [JsonProperty("grant_type")]
    public string GrantType { get; set; } = "";
}