namespace Chain_of_Responsibility;

public class KeywordHandler : SpamHandler
{
    public override bool HandleSpamCheck(Email email)
    {
        Console.WriteLine("Checking email for spam keywords");
        
        if (ContainsSpamKeywords(email.Subject) || ContainsSpamKeywords(email.Body))
        {
            Console.WriteLine("Email contains spam keywords");
            return true;
        }

        Console.WriteLine("Email contains no spam keywords");

        if (_successor is null)
        {
            return false;
        }
        
        return _successor.HandleSpamCheck(email);
    }

    private static bool ContainsSpamKeywords(string text)
    {
        return text.Contains("Nigerian Prince") || text.Contains("Million Dollars");
    }
}