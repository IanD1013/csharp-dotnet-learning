namespace DependencyInjection.Introduction.Demos.CarExample;

/// <summary>
/// Lesson 1 of the notes: the shape of the problem.
/// Every fuel type needs its own car class, because the car owns the `new`.
/// </summary>
public class PetrolCar
{
    private readonly HardWiredPetrolEngine _engine = new();

    public void StartEngine() => _engine.Start();
}

/// <summary>
/// The second fuel type. Nothing here is shared with <see cref="PetrolCar"/>,
/// even though "start the engine" is the same idea in both.
/// </summary>
public class DieselCar
{
    private readonly HardWiredDieselEngine _engine = new();

    public void StartEngine() => _engine.Start();
}

// CA1822: these mirror the course's engines, which are instance types a car news up.
// Making them static would remove the very thing the lesson is about.
#pragma warning disable CA1822
public class HardWiredPetrolEngine
{
    // Battery, induction coil, spark plug, fuel.
    public void Start() => Console.WriteLine("    PetrolEngine: spark plug fires, fuel burns.");
}

public class HardWiredDieselEngine
{
    // Ignites based on pressure.
    public void Start() => Console.WriteLine("    DieselEngine: compression ignites the fuel.");
}
#pragma warning restore CA1822
