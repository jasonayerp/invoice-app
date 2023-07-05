namespace Invoice.Api.Data.Entities;

public class InvoiceEntity
{
    public int InvoiceId { get; set; } = 0;
    public Guid Guid { get; set; } = Guid.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime UtcDate { get; set; } = DateTime.UtcNow;
    public int Status { get; set; }
    public int PaymentTerm { get; set; }
    public int BillFromAddressId { get; set; }
    public int BillToAddressId { get; set; }
    public int ClientId { get; set; }
    public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public AddressEntity BillFromAddress { get; set; } = default!;
    public AddressEntity BillToAddress { get; set; } = default!;
    public ClientEntity Client { get; set; } = default!;
    public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = new HashSet<InvoiceItemEntity>();
}
