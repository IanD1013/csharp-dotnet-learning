namespace DomeGym.Section3.Domain.UnitTests.TestUtils.Services;

public class TestDateTimeProvider(DateTime? fixedDateTime = null) : IDateTimeProvider
{
    public DateTime UtcNow => fixedDateTime ?? DateTime.UtcNow;
}
