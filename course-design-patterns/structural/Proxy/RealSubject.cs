namespace Proxy;

public class RealSubject : Subject
{
    public RealSubject()    
    {
        Console.WriteLine("Instantiating RealSubject");
    }

    public void Operation()
    {
        Console.WriteLine("Performing operation in RealSubject");
    }
}