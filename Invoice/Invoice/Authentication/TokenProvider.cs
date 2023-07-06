namespace Invoice.Authentication;

public interface ITokenProvider
{
    Task<Token> GetTokenAsync();
}
