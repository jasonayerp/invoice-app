
 namespace Invoice.Api.Data.Entities;

public class ClientEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ICollection<ClientAddressEntity> Addresses { get; set; } = new HashSet<ClientAddressEntity>();
    public ICollection<InvoiceEntity> Invoices { get; set; } = new HashSet<InvoiceEntity>();
}
