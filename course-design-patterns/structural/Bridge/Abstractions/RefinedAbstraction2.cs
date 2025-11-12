using Bridge.Implementors;

namespace Bridge.Abstractions;

public class RefinedAbstraction2(Implementor implementor) : Abstraction(implementor)
{
    public override void Foo()
    {
        Console.WriteLine("-----");
        Implementor.Print();
        Console.WriteLine("-----");
    }
}