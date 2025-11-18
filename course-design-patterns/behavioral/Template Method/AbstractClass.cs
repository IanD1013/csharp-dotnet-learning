namespace Template_Method;

public abstract class AbstractClass
{
    public void TemplateMethod()
    {
        Console.WriteLine("Before operation 1");
        PrimitiveOperation1();
        
        Console.WriteLine("Before operation 2");
        PrimitiveOperation2();
        
        Console.WriteLine("Before hook");
        Hook();
    }
    
    protected abstract void PrimitiveOperation1();
    
    protected abstract void PrimitiveOperation2();

    protected virtual void Hook()
    {
        Console.WriteLine("Hook invoked");
    }
}