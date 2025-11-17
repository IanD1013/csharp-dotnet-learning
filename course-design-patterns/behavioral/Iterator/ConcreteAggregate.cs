namespace Iterator;

public class ConcreteAggregate<T> : Aggregate<T>
{
    private readonly List<T> _items = [];
    public int Count => _items.Count;
    
    public Iterator<T> CreateIterator()
    {
        return new ConcreteIterator<T>(this);
    }

    public void Add(T value)
    {
        _items.Add(value);
    }

    public T GetItemAt(int index)
    {
        return _items[index];
    }
}