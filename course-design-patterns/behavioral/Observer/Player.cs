namespace Observer;

public class Player : IGameSubject
{
    private readonly List<IGameObserver> _observers = [];
    private int _health;
    private int _score;
    
    public void SetState(int health, int score)
    {
        _health = health;
        _score = score;
        Notify();
    }
    
    public int GetHealth()
    {
        return _health;
    }
    
    public int GetScore()
    {
        return _score;
    }
    
    public void Attach(IGameObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IGameObserver gameObserver)
    {
        _observers.Remove(gameObserver);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update();
        }
    }
}