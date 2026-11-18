using ErrorOr;

namespace DomeGym.Section5.Domain.Common.ValueObjects;

public sealed class TimeRange : ValueObject
{
    public TimeRange(TimeOnly start, TimeOnly end)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(start, end);

        Start = start;
        End = end;
    }

    public TimeOnly Start { get; }

    public TimeOnly End { get; }

    public static ErrorOr<TimeRange> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
        {
            return Error.Validation(description: "Start and end date times must be on the same day");
        }

        if (start >= end)
        {
            return Error.Validation(description: "End time must be greater than the start time");
        }

        return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public bool OverlapsWith(TimeRange other) => Start < other.End && other.Start < End;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
