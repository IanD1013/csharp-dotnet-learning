using DependencyInjection.Introduction.Demos.CarExample;
using NSubstitute;

namespace DependencyInjection.Introduction.Tests;

/// <summary>
/// Lessons 1 and 2 of the notes: the wheel-turning rig the course describes.
/// <c>Car</c> can be exercised with a hand crank; <c>PetrolCar</c> cannot be exercised at all.
/// </summary>
public class CarTests
{
    [Fact]
    public void StartEngine_ShouldStartWithoutFuel_WhenTestEngineIsInjected()
    {
        // Arrange
        var engine = new TestEngine();
        var car = new Car(engine);

        // Act
        car.StartEngine();

        // Assert
        Assert.Equal(1, engine.StartCount);
    }

    [Fact]
    public void StartEngine_ShouldDelegateToTheInjectedEngine_WhenCalled()
    {
        // Arrange
        var engine = Substitute.For<ICarEngine>();
        var car = new Car(engine);

        // Act
        car.StartEngine();

        // Assert
        engine.Received(1).Start();
    }
}
