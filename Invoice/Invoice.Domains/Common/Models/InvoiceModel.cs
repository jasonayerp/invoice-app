namespace Invoice.Domains.Common.Models;

public class InvoiceModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ClientId { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Total { get; init; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public int PaymentTermDays { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public List<InvoiceItemModel> InvoiceItems { get; set; } = new List<InvoiceItemModel>();
}

public enum InvoiceStatus
{
    Draft,
    Paid,
    Pending
}