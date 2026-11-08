using DomeGym.Section4.Domain.Common.ValueObjects;

namespace DomeGym.Section4.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public const int MaxParticipants = 10;

        public static readonly Guid Id = Guid.NewGuid();

        public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);

        public static readonly TimeRange Time = new(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9));
    }
}
