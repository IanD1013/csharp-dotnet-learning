namespace DependencyInjection.Introduction.Demos.Clock;

/// <summary>
/// Lesson 4 of the notes, the "before" version. The dependency here is the system clock,
/// which is easy to miss because it does not look like a dependency at all.
/// Two of the three branches can only be reached by waiting for the right time of day.
/// </summary>
public class TightlyCoupledGreeter
{
    // CA1822: the point of this class is that it looks dependency-free while secretly
    // depending on the clock, so it stays an instance method like its injected twin.
#pragma warning disable CA1822
    public string CreateGreetMessage()
    {
        var dateTimeNow = DateTime.Now;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
#pragma warning restore CA1822
}

/// <summary>
/// The same logic with the clock pushed behind an abstraction.
/// Production gets the real clock, a test gets whatever hour it needs.
/// </summary>
public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}

/// <summary>
/// Lesson 6 of the notes: injecting the concrete type still compiles and still runs, but the
/// constructor now names an implementation, so the only clock this greeter will ever accept is
/// the real one. Depending on the interface costs nothing and keeps the seam open.
/// </summary>
public class ConcreteGreeter
{
    private readonly SystemDateTimeProvider _dateTimeProvider;

    public ConcreteGreeter(SystemDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;
        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            _ => "Good evening"
        };
    }
}

public interface IDateTimeProvider
{
    public DateTime DateTimeNow { get; }
}

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime DateTimeNow => DateTime.Now;
}

/// <summary>
/// The test double. Every branch of the greeter becomes reachable in a single run.
/// </summary>
public class FixedDateTimeProvider : IDateTimeProvider
{
    public FixedDateTimeProvider(DateTime dateTimeNow)
    {
        DateTimeNow = dateTimeNow;
    }

    public DateTime DateTimeNow { get; }
}
