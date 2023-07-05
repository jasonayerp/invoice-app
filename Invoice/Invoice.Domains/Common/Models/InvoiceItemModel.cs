namespace Invoice.Domains.Common.Models;

public class InvoiceItemModel
{
    public int Id { get; set; } = 0;
    public int InvoiceId { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public decimal Amount { get; set; } = 0;
    public decimal TotalAmount => Quantity * Amount;
    public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
