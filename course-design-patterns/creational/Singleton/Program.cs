Singleton singleton1 = Singleton.Instance;
Singleton singleton2 = Singleton.Instance;

sealed class Singleton
{
    public static Singleton Instance { get; } = new();
    
    private Singleton()
    {
        
    }
}