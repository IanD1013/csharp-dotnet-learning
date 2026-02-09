namespace Observer;

public class GameOverScreen(Player subject) : IGameObserver
{
    private readonly Player _subject = subject;
    
    public void Update()
    {
        var health = _subject.GetHealth();
        
        if (health <= 0)
        {
            Console.WriteLine("Game Over!");
        }
    }
}