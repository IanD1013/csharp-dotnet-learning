namespace Template_Method;

public class ConcreteClass2 : AbstractClass
{
    protected override void PrimitiveOperation1()
    {
        Console.WriteLine("Primitive operation 1 called");
    }

    protected override void PrimitiveOperation2()
    {
        Console.WriteLine("Primitive operation 2 called");
    }
    
    protected override void Hook()
    {
        Console.WriteLine("Hook called in concrete class 2");
    }
}