namespace DomeGym.Section5.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Room
    {
        public const string Name = "Room 1";
        public const int MaxDailySessions = Subscription.MaxDailySessionsFreeTier;

        public static readonly Guid Id = Guid.NewGuid();
    }
}
