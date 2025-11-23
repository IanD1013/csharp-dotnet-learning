namespace Chain_of_Responsibility;

public class ConcreteHandler2 : Handler
{
    public override void Handle(string request)
    {
        if (request == "2")
        {
            Console.WriteLine("Concrete handler 2 handled request 2");
            return;
        }

        if (_successor is null)
        {
            return;
        }

        _successor.Handle(request);
    }
}