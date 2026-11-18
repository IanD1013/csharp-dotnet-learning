using DomeGym.Section5.Domain.Common.ValueObjects;

namespace DomeGym.Section5.Domain.UnitTests.TestUtils.Common;

public static class TimeRangeFactory
{
    public static TimeRange CreateFromHours(int startHour, int endHour)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(startHour);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startHour, 23);
        ArgumentOutOfRangeException.ThrowIfLessThan(endHour, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(endHour, 23);

        return new TimeRange(
            start: TimeOnly.MinValue.AddHours(startHour),
            end: TimeOnly.MinValue.AddHours(endHour));
    }
}
