namespace Invoice.Api.Data.Entities;

public class ClientEntity
{
    public int ClientId { get; set; }
    public Guid Guid { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ICollection<ClientPhoneEntity> ClientPhones { get; set; } = new HashSet<ClientPhoneEntity>();
    public ICollection<ClientAddressEntity> ClientAddresses { get; set; } = new HashSet<ClientAddressEntity>();
    public ICollection<InvoiceEntity> Invoices { get; set; } = new HashSet<InvoiceEntity>();
}
