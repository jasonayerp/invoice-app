namespace Invoice.Api.Objects;

public class Metadata
{
    public int Id { get; set; } = 0;
    public DateTime UtcCreatedDate { get; set; } = DateTime.MinValue;
    public DateTime? UtcUpdatedDate { get; set; }
    public DateTime? UtcDeletedDate { get; set; }
}
