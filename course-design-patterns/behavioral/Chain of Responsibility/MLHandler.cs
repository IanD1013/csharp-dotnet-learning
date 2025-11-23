namespace Chain_of_Responsibility;

public class MLHandler : SpamHandler
{
    public override bool HandleSpamCheck(Email email)
    {
        Console.WriteLine("Model checking if email is spam");

        if (email.Body.Length % 2 == 0)
        {
            Console.WriteLine("Model predicted email is spam");
            return true;
        }

        Console.WriteLine("Model predicted email is not spam");

        if (_successor is null)
        {
            return false;
        }

        return _successor.HandleSpamCheck(email);
    }
}