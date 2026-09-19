using DependencyInjection.Introduction.Demos.CarExample;

namespace DependencyInjection.Introduction.Demos;

/// <summary>
/// Lessons 1 and 2 of the notes: "The problem with dependencies" and
/// "Why Dependency injection is necessary".
/// </summary>
public static class CarDemo
{
    public static void Run()
    {
        HardWired();
        Console.WriteLine();
        Injected();
    }

    private static void HardWired()
    {
        Console.WriteLine("-- One class per fuel type, because the car owns the `new` --");

        new PetrolCar().StartEngine();
        new DieselCar().StartEngine();

        Console.WriteLine("  Adding electric means adding ElectricCar, then BatteryElectricCar,");
        Console.WriteLine("  then HydrogenElectricCar. The hierarchy grows with every new engine.");
        Console.WriteLine("  There is also no way to start one of these cars without a real engine,");
        Console.WriteLine("  so the wheel-turning rig cannot be tested on its own.");
    }

    private static void Injected()
    {
        Console.WriteLine("-- One Car class, engine handed in through the constructor --");

        var petrolCar = new Car(new PetrolEngine());
        var dieselCar = new Car(new DieselEngine());

        petrolCar.StartEngine();
        dieselCar.StartEngine();

        var testEngine = new TestEngine();
        var testCar = new Car(testEngine);
        testCar.StartEngine();

        Console.WriteLine($"  Same Car class in all three cases; TestEngine was started {testEngine.StartCount} time(s).");
        Console.WriteLine("  A car is now a car that *has* an engine, not a car that *is* petrol powered.");
    }
}
