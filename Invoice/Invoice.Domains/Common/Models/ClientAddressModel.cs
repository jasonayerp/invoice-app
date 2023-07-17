using Globalization.Mail;

namespace Invoice.Domains.Common.Models;

public class ClientAddressModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string City { get; set; }
    public string? Region { get; set; }
    public string PostalCode { get; set; }
    public CountryCode CountryCode { get; set; } = CountryCode.Unknown;
    public bool IsActive { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
