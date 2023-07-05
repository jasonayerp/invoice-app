namespace Invoice.Api.Data.Entities;

public class InvoiceItemEntity
{
    public int InvoiceItemId { get; set; } = 0;
    public int InvoiceId { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public decimal Amount { get; set; } = 0;
    public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public InvoiceEntity Invoice { get; set; } = default!;
}
