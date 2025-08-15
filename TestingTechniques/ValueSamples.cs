namespace TestingTechniques;

public class ValueSamples
{
    public string FullName = "Ian Dong";

    public int Age = 21;

    public DateOnly DateOfBirth = new(2000, 6, 9);

    public User AppUser = new()
    {
        FullName = "Ian Dong",
        Age = 21,
        DateOfBirth = new(2000, 6, 9)
    };
}