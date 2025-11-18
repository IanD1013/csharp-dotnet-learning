namespace Template_Method;

public class ConcreteClass1 : AbstractClass
{
    protected override void PrimitiveOperation1()
    {
        Console.WriteLine("Primitive operation 1 invoked");
    }

    protected override void PrimitiveOperation2()
    {
        Console.WriteLine("Primitive operation 2 invoked");
    }
}