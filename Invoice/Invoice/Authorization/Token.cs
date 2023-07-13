namespace Invoice.Authorization;

public class Token
{
    public string AccessToken { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public int ExpiresIn { get; set; } = 0;
    public string TokenType { get; set; } = string.Empty;
}
