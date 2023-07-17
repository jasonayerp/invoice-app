namespace Invoice.Api.Data.Entities;

public sealed class ClientAddressEntity
{
    public int ClientId { get; set; }
    public int AddressId { get; set; }
    public bool IsActive { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
