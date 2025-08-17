namespace AdvancedTechniques;

public class Greeter
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public Greeter(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateGreetMessage()
    {
        var dateTimeNow = _dateTimeProvider.DateTimeNow;

        return dateTimeNow.Hour switch
        {
            >= 5 and < 12 => "Good Morning",
            >= 12 and < 18 => "Good Afternoon",
            _ => "Good Evening"
        };
    }
}