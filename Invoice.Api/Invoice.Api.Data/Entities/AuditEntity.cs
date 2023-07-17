namespace Invoice.Api.Data.Entities;

public sealed class AuditEntity
{
    public int AuditId { get; set; }
    public string Domain { get; set; } = "";
    public string Action { get; set; } = "";
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime UtcCreatedDate { get; set; }
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
