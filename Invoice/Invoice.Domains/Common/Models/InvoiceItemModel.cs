namespace Invoice.Domains.Common.Models;

public class InvoiceItemModel
{
    public int Id { get; set; } = 0;
    public int InvoiceId { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public decimal Amount { get; set; } = 0m;
    public decimal TotalAmount => Quantity * Amount;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
