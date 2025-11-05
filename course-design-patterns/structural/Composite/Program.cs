using Composite;
using ComponentClass = Composite.Component;
using CompositeClass = Composite.Composite;

ComponentClass root = new CompositeClass();
ComponentClass leafA = new Leaf();
root.Add(leafA);

ComponentClass childComposite = new CompositeClass();
ComponentClass leafB = new Leaf();
ComponentClass leafC = new Leaf();      
childComposite.Add(leafB);  
childComposite.Add(leafC);

root.Add(childComposite);

root.Operation();
leafA.Operation();