using DomeGym.Section5.Domain.Common.ValueObjects;
using DomeGym.Section5.Domain.SessionAggregate;

namespace DomeGym.Section5.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Session
    {
        public const string Name = "Zoomba Session";
        public const string Description = "The best zoomba yay";
        public const int MaxParticipants = 10;

        public static readonly Guid Id = Guid.NewGuid();

        public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);

        public static readonly TimeRange Time = new(
            TimeOnly.MinValue.AddHours(8),
            TimeOnly.MinValue.AddHours(9));

        public static readonly IReadOnlyList<SessionCategory> Categories = [SessionCategory.Zoomba];
    }
}
