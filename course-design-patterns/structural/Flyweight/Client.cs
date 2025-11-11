namespace Flyweight;
using Key = string;

public class Client(FlyweightFactory flyweightFactory)
{
    private readonly FlyweightFactory _flyweightFactory = flyweightFactory;
    
    public void Operation(string extrinsicState)
    {
        Key key1 = extrinsicState[0].ToString();
        Key key2 = extrinsicState[0].ToString();
        
        Flyweight flyweight1 = _flyweightFactory.GetFlyweight(key1);
        Flyweight flyweight2 = _flyweightFactory.GetFlyweight(key2);
        
        flyweight1.Operation(extrinsicState);
        flyweight2.Operation(extrinsicState);
    }
}