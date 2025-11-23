namespace Chain_of_Responsibility;

public class SpamDetectionSystem
{
    private readonly SpamHandler _chain;
    
    public SpamDetectionSystem()
    {
        _chain = new KeywordHandler();
        var blacklistHandler = new BlacklistHandler();
        var mlHandler = new MLHandler();
        
        _chain.SetSuccessor(blacklistHandler);
        blacklistHandler.SetSuccessor(mlHandler);
    }
    
    public bool CheckForSpam(Email email)
    {
        return _chain.HandleSpamCheck(email);
    }
}