
 namespace Invoice.Api.Data.Entities;

public class ClientEntity
{
    public int ClientId { get; set; }
    public Guid Guid { get; set; }
    public int OrganizationId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ICollection<ClientAddressEntity> Addresses { get; set; } = new HashSet<ClientAddressEntity>();
    public ICollection<InvoiceEntity> Invoices { get; set; } = new HashSet<InvoiceEntity>();
}
