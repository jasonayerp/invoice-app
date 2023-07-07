using Invoice.Auth0;
using Invoice.Authentication;
using Invoice.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Invoice.Web.Authentication;

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
        var cacheItem = await _memoryCache.GetOrCreateAsync("token", async (cacheOptions) =>
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                string? endpoint = _configurationReader.GetValue("Endpoint");
                string? clientId = _configurationReader.GetValue("ClientId");
                string? clientSecret = _configurationReader.GetValue("ClientSecret");
                string? audience = _configurationReader.GetValue("Audience");
                string? grantType = _configurationReader.GetValue("GrantType");

                if (string.IsNullOrEmpty(endpoint)) throw new ArgumentNullException("Endpoint cannot be null or empty.");
                if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("ClientId cannot be null or empty.");
                if (string.IsNullOrEmpty(clientSecret)) throw new ArgumentNullException("ClientSecret cannot be null or empty.");
                if (string.IsNullOrEmpty(audience)) throw new ArgumentNullException("Audience cannot be null or empty.");
                if (string.IsNullOrEmpty(grantType)) throw new ArgumentNullException("GrantType cannot be null or empty.");

                List<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("ClientSecret", clientSecret),
                    new KeyValuePair<string, string>("audience", audience),
                    new KeyValuePair<string, string>("grant_type", grantType)
                };

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
                request.Content = new FormUrlEncodedContent(nameValueCollection);

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) throw new Exception(response.StatusCode.ToString());

                JsonWebToken? result = JsonConvert.DeserializeObject<JsonWebToken>(await response.Content.ReadAsStringAsync());

                if (result == null) throw new ArgumentNullException("JsonWebToken cannot be null.");

                Token token = new Token
                {
                    AccessToken = result.AccessToken,
                    Scope = result.Scope,
                    TokenType = result.TokenType,
                    ExpiresIn = result.ExpiresIn
                };

                cacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(token.ExpiresIn - 300);

                return JsonConvert.SerializeObject(token);
            }
        });

        return JsonConvert.DeserializeObject<Token>(cacheItem);
    }
}
