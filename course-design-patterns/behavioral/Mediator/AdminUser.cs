namespace Mediator;

public class AdminUser(string name) : User(name)
{
    public override void Send(string message)
    {
        Console.WriteLine($"Admin {Name} announces: {message}");
        _mediator.Notify(this, $"ADMIN MESSAGE: {message}");
    }

    public override void Receive(string message, string senderName)
    {
        Console.WriteLine($"Admin {Name} received: {message} from {senderName}");
    }
    
    internal void BroadcastAlert(string alert)
    {
        Console.WriteLine($"Admin {Name} broadcasts: {alert}");
        _mediator.Notify(this, $"ALERT: {alert}");
    }
}