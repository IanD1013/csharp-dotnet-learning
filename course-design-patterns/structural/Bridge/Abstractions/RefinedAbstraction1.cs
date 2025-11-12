using Bridge.Implementors;

namespace Bridge.Abstractions;

public class RefinedAbstraction1(Implementor implementor) : Abstraction(implementor)
{
    public override void Foo()
    {
        Implementor.Print();
    }
}