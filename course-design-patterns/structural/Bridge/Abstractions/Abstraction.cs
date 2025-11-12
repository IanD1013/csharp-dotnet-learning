using Bridge.Implementors;

namespace Bridge.Abstractions;

public abstract class Abstraction(Implementor implementor)
{
    protected Implementor Implementor { get; init; } = implementor;

    public abstract void Foo();
}