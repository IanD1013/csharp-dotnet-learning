using ErrorOr;

namespace DomeGym.Section3.Domain;

public sealed record TimeRange
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
        if (start.Date != end.Date || start >= end)
        {
            return Error.Validation();
        }

        return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public bool OverlapsWith(TimeRange other) => Start < other.End && other.Start < End;
}
