namespace Invoice.Services;

public interface IDateTimeOffsetService
{
    DateTimeOffset Now { get; }
    DateTimeOffset UtcNow { get; }
}

public class DateTimeOffsetService : IDateTimeOffsetService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
