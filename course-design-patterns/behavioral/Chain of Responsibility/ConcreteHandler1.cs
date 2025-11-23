namespace Chain_of_Responsibility;

public class ConcreteHandler1 : Handler
{
    public override void Handle(string request)
    {
        if (request == "1")
        {
            Console.WriteLine("Concrete handler 1 handled request 1");
            return;
        }

        if (_successor is null)
        {
            return;
        }

        _successor.Handle(request);
    }
}