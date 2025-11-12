namespace Proxy;

public class Proxy : Subject
{
    private RealSubject? _realSubject = null;
    
    public Proxy()    
    {
        Console.WriteLine("Instantiating Proxy");
    }

    public void Operation()
    {
        Console.WriteLine("Performing operation in Proxy");

        if (_realSubject is null)
        {
            Console.WriteLine("Real object is null, creating it");
            _realSubject = new RealSubject();
        }

        Console.WriteLine("Logging before operation");
        _realSubject.Operation();
        Console.WriteLine("Logging after operation");
    }
}