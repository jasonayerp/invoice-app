namespace Invoice.Api.Data.Entities;

public sealed class InvoiceEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int ClientId { get; set; }
    public string Number { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int Status { get; set; }
    public int PaymentTermDays { get; set; }
    public decimal Total { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ClientEntity Client { get; set; } = default!;
    public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = new HashSet<InvoiceItemEntity>();
}
