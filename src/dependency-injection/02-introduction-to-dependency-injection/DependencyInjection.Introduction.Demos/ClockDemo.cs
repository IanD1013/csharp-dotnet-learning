using DependencyInjection.Introduction.Demos.Clock;

namespace DependencyInjection.Introduction.Demos;

/// <summary>
/// Lessons 4 and 6 of the notes: the hidden system-level dependency, and what changes when the
/// constructor names a concrete type instead of an interface.
/// </summary>
public static class ClockDemo
{
    public static void Run()
    {
        HardWired();
        Console.WriteLine();
        Injected();
        Console.WriteLine();
        Concrete();
    }

    private static void HardWired()
    {
        Console.WriteLine("-- DateTime.Now inside the class --");

        var greeter = new TightlyCoupledGreeter();

        Console.WriteLine($"  Right now it says: \"{greeter.CreateGreetMessage()}\"");
        Console.WriteLine("  The other two branches are unreachable until the clock says so,");
        Console.WriteLine("  which means the test suite passes or fails depending on the time of day.");
    }

    private static void Injected()
    {
        Console.WriteLine("-- IDateTimeProvider handed in through the constructor --");

        DateTime[] hours =
        [
            new DateTime(2026, 9, 19, 8, 0, 0),
            new DateTime(2026, 9, 19, 14, 0, 0),
            new DateTime(2026, 9, 19, 21, 0, 0)
        ];

        foreach (var moment in hours)
        {
            var greeter = new Greeter(new FixedDateTimeProvider(moment));
            Console.WriteLine($"  {moment:HH:mm} -> \"{greeter.CreateGreetMessage()}\"");
        }

        var systemGreeter = new Greeter(new SystemDateTimeProvider());
        Console.WriteLine($"  system clock -> \"{systemGreeter.CreateGreetMessage()}\"");
        Console.WriteLine("  All three branches covered in one run, and production still uses the real clock.");
    }

    private static void Concrete()
    {
        Console.WriteLine("-- Constructor naming the implementation instead of the interface --");

        var greeter = new ConcreteGreeter(new SystemDateTimeProvider());

        Console.WriteLine($"  It runs: \"{greeter.CreateGreetMessage()}\"");
        Console.WriteLine("  But `new ConcreteGreeter(new FixedDateTimeProvider(...))` does not compile,");
        Console.WriteLine("  so the seam is gone. Prefer interfaces: they mock cleanly and force no hierarchy.");
    }
}
