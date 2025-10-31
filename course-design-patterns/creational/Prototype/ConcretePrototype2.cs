namespace Prototype;

public class ConcretePrototype2 : IPrototype
{
    public IPrototype Clone()
    {
        return new ConcretePrototype2();
    }
}