namespace DomeGym.Section3.Domain;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
