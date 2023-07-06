using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Invoice.Auth0;

[JsonObject]
public class JsonWebToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = string.Empty;
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } = 0;
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;
}
