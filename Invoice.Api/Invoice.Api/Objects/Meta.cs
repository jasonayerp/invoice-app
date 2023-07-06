namespace Invoice.Api.Objects;

public class Meta
{
    public DateTime UtcCreatedDate { get; set; } = DateTime.MinValue;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
