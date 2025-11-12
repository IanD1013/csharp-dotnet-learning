using Bridge;
using Bridge.Abstractions;
using Bridge.Implementors;

Abstraction abstraction = new RefinedAbstraction1(new ConcreteImplementor1());
abstraction.Foo();