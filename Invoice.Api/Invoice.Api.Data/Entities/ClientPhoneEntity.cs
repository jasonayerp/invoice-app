namespace Invoice.Api.Data.Entities;

public sealed class ClientPhoneEntity
{
    public int ClientPhoneId { get; set; }
    public int ClientId { get; set; }
    public string CountryCode { get; set; }
    public string Number { get; set; }
    public string? ExtensionNumber { get; set; }
    public bool IsActive { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ClientEntity Client { get; set; } = default!;
}
