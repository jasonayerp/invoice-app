using Invoice.Configuration;
using Invoice.Domains.Common.Models;
using SmartyStreets;
using SmartyStreets.USStreetApi;

namespace Invoice.Web.Domains.Common.Services;

public interface IAddressVerificationService
{
    Task<AddressModel?> VerifyAddressAsync(AddressModel address);
}

public class SmartyAddressVerificationService : IAddressVerificationService
{
    private readonly IConfigurationReader _configurationReader;

    public SmartyAddressVerificationService(IConfigurationReader configurationReader)
    {
        _configurationReader = configurationReader;
    }

    public async Task<AddressModel?> VerifyAddressAsync(AddressModel address)
    {
        return await Task.Run(() =>
        {
            var authId = _configurationReader.GetValue("SmartyAuthId");
            var authToken = _configurationReader.GetValue("SmartyAuthToken");

            var client = new ClientBuilder(authId, authToken)
                .WithLicense(new List<string> { "us-core-cloud" })
                .BuildUsStreetApiClient();

            var lookup = new Lookup
            {
                Street = address.AddressLine1,
                Street2 = address.AddressLine2 ?? null,
                City = address.City,
                State = address.Region,
                ZipCode = address.PostalCode,
                MaxCandidates = 1,
                MatchStrategy = Lookup.ENHANCED
            };

            client.Send(lookup);

            var candidate = lookup.Result.FirstOrDefault();

            if (candidate == null) return null;

            var parts = candidate.LastLine.Split(' ');

            return new AddressModel
            {
                AddressLine1 = candidate.DeliveryLine1,
                AddressLine2 = candidate.DeliveryLine2 ?? null,
                City = string.Join(' ', parts.Take(parts.Length - 2)),
                Region = parts.ElementAt(parts.Length - 2),
                PostalCode = parts.ElementAt(parts.Length - 1),
            };
        });
    }
}
