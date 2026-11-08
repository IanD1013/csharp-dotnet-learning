using DomeGym.Section4.Domain.Common.Interfaces;

namespace DomeGym.Section4.Domain.UnitTests.TestUtils.Services;

public class TestDateTimeProvider(DateTime? fixedDateTime = null) : IDateTimeProvider
{
    public DateTime UtcNow => fixedDateTime ?? DateTime.UtcNow;
}
