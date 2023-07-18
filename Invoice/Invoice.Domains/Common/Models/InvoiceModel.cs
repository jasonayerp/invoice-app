namespace Invoice.Domains.Common.Models;

public class InvoiceModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ClientId { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime UtcDate { get; set; }
    public DateTime UtcDueDate { get; set; }
    public decimal Amount => InvoiceItems.Sum(x => x.Amount);
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public int NetPaymentTermDays { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public List<InvoiceItemModel> InvoiceItems { get; set; } = new List<InvoiceItemModel>();
}

public enum InvoiceStatus
{
    Draft,
    Paid,
    Pending
}