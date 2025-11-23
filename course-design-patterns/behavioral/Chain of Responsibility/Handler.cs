namespace Chain_of_Responsibility;

public abstract class Handler
{
    protected Handler? _successor = null;
    
    public void SetSuccessor(Handler successor)
    {
        _successor = successor;
    }

    public abstract void Handle(string request);
}