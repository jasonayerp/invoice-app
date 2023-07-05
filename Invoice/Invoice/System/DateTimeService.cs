namespace Invoice.System;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}

internal class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
