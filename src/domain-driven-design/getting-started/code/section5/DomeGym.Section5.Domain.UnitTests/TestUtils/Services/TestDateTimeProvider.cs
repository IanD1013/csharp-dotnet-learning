using DomeGym.Section5.Domain.Common.Interfaces;

namespace DomeGym.Section5.Domain.UnitTests.TestUtils.Services;

public class TestDateTimeProvider(DateTime? fixedDateTime = null) : IDateTimeProvider
{
    public DateTime UtcNow => fixedDateTime ?? DateTime.UtcNow;
}
