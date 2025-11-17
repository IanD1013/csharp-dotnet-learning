namespace Iterator;

public class ConcreteIterator<T>(ConcreteAggregate<T> aggregate) : Iterator<T>
{
    private int _currentIndex = -1;
    
    public bool HasNext()
    {
        return _currentIndex < aggregate.Count - 1;
    }

    public T Next()
    {
        if (!HasNext())
        {
            throw new InvalidOperationException();
        }
        
        _currentIndex++;
        return aggregate.GetItemAt(_currentIndex);
    }

    public void Reset()
    {
        _currentIndex = -1;
    }
}