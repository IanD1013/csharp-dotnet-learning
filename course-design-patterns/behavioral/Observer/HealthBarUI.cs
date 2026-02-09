namespace Observer;

public class HealthBarUI(Player subject) : IGameObserver
{
    private readonly Player _subject = subject;
    public void Update()
    {
        var health = _subject.GetHealth();
        
        Console.WriteLine($"Health: {health}");
    }
}