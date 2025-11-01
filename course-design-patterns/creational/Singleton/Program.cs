var singleton1 = Singleton.Instance;
var singleton2 = Singleton.Instance;

sealed class Singleton
{
    private static readonly Lazy<Singleton> _lazyInstance = new(() => new Singleton());

    public static Singleton Instance => _lazyInstance.Value;

    private Singleton()
    {
        Console.WriteLine("Instantiating singleton");
    }
}