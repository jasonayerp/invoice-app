using Invoice.Domains.Common.Enums;

namespace Invoice.Domains.Common.Models;

public class InvoiceModel
{
    public int Id { get; set; } = 0;
    public Guid Guid { get; set; } = Guid.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime UtcDate { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public decimal Amount => InvoiceItems.Sum(e => e.Amount);
    public InvoiceStatusEnum Status { get; set; } = InvoiceStatusEnum.Draft;
    public PaymentTermEnum PaymentTerm { get; set; } = PaymentTermEnum.PIA;
    public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public AddressModel BillFromAddress { get; set; } = default!;
    public AddressModel BillToAddress { get; set; } = default!;
    public ClientModel Client { get; set; } = default!;
    public ICollection<InvoiceItemModel> InvoiceItems { get; set; } = new HashSet<InvoiceItemModel>();
}
