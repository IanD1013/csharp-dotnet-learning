namespace Mediator;

public interface IChatMediator
{
    void Notify(User sender, string @event);
}