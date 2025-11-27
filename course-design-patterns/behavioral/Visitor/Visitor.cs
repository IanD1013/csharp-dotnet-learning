namespace Visitor;

public interface Visitor
{
    void Visit(ConcreteElement1 element);
    void Visit(ConcreteElement2 element);
}