using Invoice.Authorization;
using Invoice.Configuration;
using Invoice.Web.Core.Helpers;

namespace Invoice.Web.Core.Factories;

public interface IInvoiceHttpClientFactory
{
    HttpClient CreateClient();
}

public class InvoiceHttpClientFactory : IInvoiceHttpClientFactory
{
    private readonly IConfigurationReader _configurationReader;
    private readonly ITokenProvider _tokenProvider;

    public InvoiceHttpClientFactory(IConfigurationReader configurationReader, ITokenProvider tokenProvider)
    {
        _configurationReader = configurationReader;
        _tokenProvider = tokenProvider;
    }

    public HttpClient CreateClient()
    {
        var client = new HttpClient();

        var baseAddress = _configurationReader.GetValue("ApiBaseUri");
        if (string.IsNullOrEmpty(baseAddress)) throw new ArgumentNullException("ApiBaseUri is required.");

        var token = AsyncHelper.RunSync(async () => await _tokenProvider.GetTokenAsync());

        client.BaseAddress = new Uri(baseAddress);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(token.TokenType, token.AccessToken);

        return client;
    }
}
