using Visitor;

Element element1 = new ConcreteElement1();
Element element2 = new ConcreteElement2();

Visitor.Visitor visitor1 = new ConcreteVisitor1();
Visitor.Visitor visitor2 = new ConcreteVisitor2();

element1.Accept(visitor1);
element2.Accept(visitor1);

element1.Accept(visitor2);
element2.Accept(visitor2);