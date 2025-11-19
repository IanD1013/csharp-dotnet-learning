namespace Observer;

public class ConcreteSubject : Subject
{
    private readonly List<Observer> _observers = [];
    private string _state = "";
    
    public void SetState(string state)
    {
        _state = state;
        Notify();
    }
    
    public string GetState()
    {
        return _state;
    }
    
    public void Attach(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Detach(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update();
        }
    }
}