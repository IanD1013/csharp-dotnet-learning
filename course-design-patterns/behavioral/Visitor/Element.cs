namespace Visitor;

public interface Element
{
     void Accept(Visitor visitor);
}