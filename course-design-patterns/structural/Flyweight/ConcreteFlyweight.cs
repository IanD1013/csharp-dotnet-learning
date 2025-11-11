namespace Flyweight;

public class ConcreteFlyweight(string intrinsicState) : Flyweight
{
    private readonly string intrinsicState = intrinsicState;
    
    public void Operation(string extrinsicState)
    {
        Console.WriteLine($"ConcreteFlyweight: IntrinsicState: {intrinsicState}, ExtrinsicState: {extrinsicState}");
    }
}