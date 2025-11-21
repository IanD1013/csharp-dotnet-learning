namespace Observer;

public interface IGameSubject
{
    void Attach(IGameObserver observer);
    void Detach(IGameObserver observer);
    void Notify();
}