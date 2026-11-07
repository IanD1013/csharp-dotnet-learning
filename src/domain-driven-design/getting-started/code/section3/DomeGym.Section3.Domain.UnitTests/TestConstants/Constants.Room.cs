namespace DomeGym.Section3.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Room
    {
        public const int MaxDailySessions = Subscription.MaxDailySessionsFreeTier;

        public static readonly Guid Id = Guid.NewGuid();
    }
}
