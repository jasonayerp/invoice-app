namespace Invoice.Domains.Common.Models;

public class InvoiceModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ClientId { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
    public decimal Amount => InvoiceItems.Sum(x => x.Amount);
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public int PaymentTermDays { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
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