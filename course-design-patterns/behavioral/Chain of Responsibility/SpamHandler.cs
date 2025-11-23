namespace Chain_of_Responsibility;

public abstract class SpamHandler
{
    protected SpamHandler? _successor = null;
    
    public void SetSuccessor(SpamHandler successor)
    {
        _successor = successor;
    }

    public abstract bool HandleSpamCheck(Email email);
}