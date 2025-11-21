namespace Mediator;

public interface Mediator
{
    void Notify(Colleague sender, string @event);
}