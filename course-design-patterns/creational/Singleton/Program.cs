Console.WriteLine("Before accessing instance");
Singleton singleton1 = Singleton.Instance;
Console.WriteLine("After accessing instance");
Singleton singleton2 = Singleton.Instance;

sealed class Singleton
{
    private static Singleton _instance = null!;

    public static Singleton Instance
    {
        get
        {
            if (_instance is null)
            {
                _instance = new Singleton();
            }
            
            return _instance;
        }
    } 
    
    private Singleton()
    {
        Console.WriteLine("Instantiating singleton");
    }
}