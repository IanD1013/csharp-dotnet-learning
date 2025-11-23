namespace Chain_of_Responsibility;

public class BlacklistHandler : SpamHandler
{
    public override bool HandleSpamCheck(Email email)
    {
        Console.WriteLine("Checking if email sender is blacklisted");
        
        if (IsBlacklisted(email.Sender))
        {
            Console.WriteLine("Email sender is blacklisted");
            return true;
        }

        Console.WriteLine("Email sender is not blacklisted");

        if (_successor is null)
        {
            return false;
        }

        return _successor.HandleSpamCheck(email);
    }

    private static bool IsBlacklisted(string sender)
    {
        return sender.Contains("Spam");
    }
}