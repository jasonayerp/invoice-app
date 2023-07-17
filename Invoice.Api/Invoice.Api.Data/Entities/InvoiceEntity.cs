using Invoice.Api.Data.Documents;

namespace Invoice.Api.Data.Entities;

public sealed class InvoiceEntity
{
    public int InvoiceId { get; set; }
    public Guid Guid { get; set; }
    public int ClientId { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public DateTime UtcDate { get; set; }
    public DateTime UtcDueDate { get; set; }
    public int Status { get; set; }
    public int PaymentTerm { get; set; }    
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ClientEntity Client { get; set; } = default!;
    public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = new HashSet<InvoiceItemEntity>();
}
