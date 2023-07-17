using Invoice.Domains.Common.Enums;

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
    public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.Draft;
    public PaymentTermEnum PaymentTerm { get; set; } = PaymentTermEnum.PIA;
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public List<InvoiceItemModel> InvoiceItems { get; set; } = new List<InvoiceItemModel>();
}
