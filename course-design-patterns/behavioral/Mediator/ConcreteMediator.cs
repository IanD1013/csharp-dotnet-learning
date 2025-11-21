namespace Mediator;

public class ConcreteMediator : Mediator
{
    private readonly Colleague1 _colleague1;
    private readonly Colleague2 _colleague2;

    public ConcreteMediator(Colleague1 colleague1, Colleague2 colleague2)
    {
        _colleague1 = colleague1;
        _colleague1.SetMediator(this);
        _colleague2 = colleague2;
        _colleague2.SetMediator(this);
    }
    
    public void Notify(Colleague sender, string @event)
    {
        Console.WriteLine($"Concrete mediator notified by {sender.GetType().Name}");

        if (sender == _colleague1)
        {
            this._colleague2.Receive(@event);
        }
        else if (sender == _colleague2)
        {
            this._colleague1.Receive(@event);
        }
    }
}