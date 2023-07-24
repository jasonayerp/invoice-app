namespace Invoice.Api.Data.Entities;

public class InvoiceItemEntity
{
    public int Id { get; set; }
    public int InvoiceId { get; set; } 
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public InvoiceEntity Invoice { get; set; } = default!;
}
