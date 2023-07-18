namespace Invoice.Api.Data.Entities;

public class InvoiceItemEntity
{
    public int InvoiceItemId { get; set; }
    public int InvoiceId { get; set; } 
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public InvoiceEntity Invoice { get; set; } = default!;
}
