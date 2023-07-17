namespace Invoice.Api.Data.Entities;

public sealed class ClientPhoneEntity
{
    public int ClientId { get; set; }
    public int PhoneId { get; set; }
    public bool IsActive { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ClientEntity Client { get; set; } = default!;
    public PhoneEntity Phone { get; set; } = default!;
}
