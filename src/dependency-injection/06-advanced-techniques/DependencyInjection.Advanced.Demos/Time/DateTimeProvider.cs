namespace DependencyInjection.Advanced.Demos.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime DateTimeNow => DateTime.Now;

    public DateTime DateTimeUtcNow => DateTime.UtcNow;
}
