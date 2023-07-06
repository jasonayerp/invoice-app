using TimeZoneConverter;

namespace Invoice.Api.Helpers;

public static class TimeZoneHelper
{
    public static DateTime ToLocalTime(DateTime utcDateTime, string timeZone)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime,
        TimeZoneInfo.FindSystemTimeZoneById(TZConvert.IanaToWindows(timeZone)));
    }
    public static DateTime ToUtcTime(DateTime localDateTime, string timezone)
    {
        localDateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Unspecified);
        return TimeZoneInfo.ConvertTimeToUtc(localDateTime,
          TimeZoneInfo.FindSystemTimeZoneById(TZConvert.IanaToWindows(timezone)));
    }
}
