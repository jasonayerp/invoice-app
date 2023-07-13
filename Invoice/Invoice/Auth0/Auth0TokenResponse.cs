using Newtonsoft.Json;

namespace Invoice.Auth0;

[JsonObject]
public class Auth0TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    [JsonProperty("scope")]
    public string Scope { get; set; } = string.Empty;
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; } = 0;
    [JsonProperty("token_type")]
    public string TokenType { get; set; } = string.Empty;
}
