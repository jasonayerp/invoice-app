namespace Invoice.Authorization;

public interface ITokenProvider
{
    Task<Token> GetTokenAsync();
}
