namespace Facade;

public class SmartTVDevice : IDevice
{
    public async Task<Connection> ConnectionAsync()
    {
        Console.WriteLine("TV is off");
        throw new NotImplementedException();
    }

    public async Task<Connection> TurnOnAsync()
    {
        Console.WriteLine("Turning on TV");

        return new Connection();
    }
}
