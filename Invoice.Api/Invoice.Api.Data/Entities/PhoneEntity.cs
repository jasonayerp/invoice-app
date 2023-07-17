namespace Invoice.Api.Data.Entities;

public sealed class PhoneEntity
{
    public int PhoneId { get; set; }
    public Guid Guid { get; set; } = Guid.NewGuid();
    public string CountryCode { get; set; } = "";
    public string Number { get; set; } = "";
    public string ExtensionNumber { get; set; } = "";
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ClientPhoneEntity ClientPhone { get; set; } = default!;
}
