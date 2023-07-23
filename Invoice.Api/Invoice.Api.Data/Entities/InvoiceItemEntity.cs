namespace Invoice.Api.Data.Entities;

public class InvoiceItemEntity
{
    public int InvoiceItemId { get; set; }
    public int InvoiceId { get; set; } 
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public InvoiceEntity Invoice { get; set; } = default!;
}
