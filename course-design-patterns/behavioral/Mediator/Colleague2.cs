namespace Mediator;

public class Colleague2 : Colleague
{
    public void Operation2()
    {
        Console.WriteLine("Colleague2 does operation2");
        
        _mediator.Notify(this, "Colleague2 did operation2");
    }

    public void Receive(string @event)
    {
        Console.WriteLine($"Colleague2 received: {@event}");
    }
}