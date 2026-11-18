using DomeGym.Section5.Domain.Common.Interfaces;

namespace DomeGym.Section5.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
