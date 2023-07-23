using Globalization.Mail;

namespace Invoice.Domains.Common.Models;

public class ClientAddressModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? Line3 { get; set; }
    public string? Line4 { get; set; }
    public string City { get; set; }
    public string? Region { get; set; }
    public string PostalCode { get; set; }
    public CountryCode CountryCode { get; set; } = CountryCode.Unknown;
    public bool IsDefault { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
