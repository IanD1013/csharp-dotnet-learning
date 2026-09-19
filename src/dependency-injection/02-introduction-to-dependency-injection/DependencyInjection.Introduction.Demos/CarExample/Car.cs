namespace DependencyInjection.Introduction.Demos.CarExample;

/// <summary>
/// Lesson 2 of the notes: one car, any engine.
/// The car depends on the <see cref="ICarEngine"/> abstraction and takes it through the
/// constructor, so the caller decides which engine goes in.
/// </summary>
public class Car
{
    private readonly ICarEngine _carEngine;

    public Car(ICarEngine carEngine)
    {
        _carEngine = carEngine;
    }

    public void StartEngine() => _carEngine.Start();

    // More methods.
}

public interface ICarEngine
{
    void Start();

    // Keep adding.
}

public class PetrolEngine : ICarEngine
{
    // Battery, induction coil, spark plug, fuel.
    public void Start() => Console.WriteLine("    PetrolEngine: spark plug fires, fuel burns.");
}

public class DieselEngine : ICarEngine
{
    // Ignites based on pressure.
    public void Start() => Console.WriteLine("    DieselEngine: compression ignites the fuel.");
}

/// <summary>
/// The test double from the lesson: hand cranking. No fuel, no workshop, no setup.
/// This is what makes the wheel-turning rig testable.
/// </summary>
public class TestEngine : ICarEngine
{
    public int StartCount { get; private set; }

    public void Start()
    {
        StartCount++;
        Console.WriteLine("    TestEngine: hand cranked, no fuel needed.");
    }
}
