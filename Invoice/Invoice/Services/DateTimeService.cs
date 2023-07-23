namespace Invoice.Services;

public interface IDateTimeService
{
    DateTimeOffset Now { get; }
    DateTimeOffset UtcNow { get; }
}

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
