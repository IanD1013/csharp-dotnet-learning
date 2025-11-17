namespace Strategy;

public class Context
{
    private Strategy? _strategy;
    
    public void SetStrategy(Strategy strategy)
    {
        _strategy = strategy;
    }
    
    public void ExecuteStrategy()
    {
        _strategy?.Execute();
    }
}