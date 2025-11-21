namespace Observer;

public class ScoreUI(Player subject) : IGameObserver
{
    private readonly Player _subject = subject;
    
    public void Update()
    {
        var score = _subject.GetScore();

        Console.WriteLine($"Score: {score}");
    }
}