namespace Invoice.Domains.Common.Models;

public class ClientModel
{
    public int Id { get; set; } = 0;
    public Guid Guid { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime UtcCreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }

    public ICollection<InvoiceModel> Invoices { get; set; } = new HashSet<InvoiceModel>();
}
