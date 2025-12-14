var singleton1 = Singleton.Instance;
var singleton2 = Singleton.Instance;

sealed class Singleton
{
    public static string ClassName;
    public static Singleton Instance => Nested.Instance;

    private class Nested
    {
        public static Singleton Instance { get; } = new();
        
        static Nested()
        {
        }
    }

    private Singleton()
    {
    }

    static Singleton()
    {
    }
}

// static ctor -> init fields or props only when any one of them is used