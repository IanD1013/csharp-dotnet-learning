namespace DependencyInjection.Advanced.Demos.Time;

public interface IDateTimeProvider
{
    DateTime DateTimeNow { get; }

    DateTime DateTimeUtcNow { get; }
}
