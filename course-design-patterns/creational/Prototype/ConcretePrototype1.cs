namespace Prototype;

public class ConcretePrototype1 : IPrototype
{
    public IPrototype Clone()
    {
        return new ConcretePrototype1();
    }
}