namespace Chain_of_Responsibility;

public class ConcreteHandler3 : Handler
{
    public override void Handle(string request)
    {
        if (request == "3")
        {
            Console.WriteLine("Concrete handler 3 handled request 3");
            return;
        }

        if (_successor is null)
        {
            return;
        }

        _successor.Handle(request);
    }
}