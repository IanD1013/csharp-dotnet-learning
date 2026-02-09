using Composite;
using Composite.Dometrain;
using ComponentClass = Composite.Component;
using CompositeClass = Composite.Composite;

#region CompositePattern

// ComponentClass root = new CompositeClass();
// ComponentClass leafA = new Leaf();
// root.Add(leafA);
//
// ComponentClass childComposite = new CompositeClass();
// ComponentClass leafB = new Leaf();
// ComponentClass leafC = new Leaf();      
// childComposite.Add(leafB);  
// childComposite.Add(leafC);
//
// root.Add(childComposite);
//
// root.Operation();
// leafA.Operation();

#endregion


#region DometrainExample

LearningResource root = new Bundle(name: "Zero to Hero: Clean Architecture");

LearningResource leafA = new Course(
    name: "Getting Started: Clean Architecture",
    duration: TimeSpan.FromHours(3),
    price: 100);

LearningResource leafB = new Course(
    name: "Deep Dive: Clean Architecture",
    duration: TimeSpan.FromHours(4),
    price: 110);

root.Add(leafA);
root.Add(leafB);

Console.WriteLine(root.GetPrice());

#endregion