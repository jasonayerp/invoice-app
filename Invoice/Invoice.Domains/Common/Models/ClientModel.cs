namespace Invoice.Domains.Common.Models;

public class ClientModel
{
    public int Id { get; set; } = 0;
    public Guid Guid { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ClientAddressModel? DefaultAddress => Addresses.SingleOrDefault(e => e.IsDefault);
    public List<ClientAddressModel> Addresses { get; set; } = new List<ClientAddressModel>();
    public List<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
}
