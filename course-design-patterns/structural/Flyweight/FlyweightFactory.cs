namespace Flyweight;
using Key = string;

public class FlyweightFactory
{
    private readonly Dictionary<Key, Flyweight> _flyweights = [];
    
    public Flyweight GetFlyweight(Key key)
    {
        if (!_flyweights.ContainsKey(key))
        {
            _flyweights[key] = new ConcreteFlyweight(intrinsicState: key);
        }
        
        return _flyweights[key];
    }
}