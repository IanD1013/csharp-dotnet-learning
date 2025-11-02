namespace FactoryMethod.SimpleFactoryPattern;

#region A concrete class that creates a concrete class

class PasswordFactory
{
    public static Password Generate(/* .. */)
    {
        // some complex logic
        return new Password();
    }
}

#endregion

#region A static factory method - a static method defined in a concrete type, that creates that type

class Password
{
    // private Password() { } -> limit anyone creating an instance of this class
    
    public static Password Generate(/* .. */)
    {
        // some complex logic
        return new Password();
    }
}

#endregion

#region A concrete class that creates a concrete instance from a set of defined classes

// using inheritance or interface implementation
class PasswordFactory2
{
    public static IPassword Generate(ushort length)
    {
        return length < 5
            ? new SimplePassword()
            : new ComplexPassword();
    }
}

interface IPassword;  
class SimplePassword : IPassword;
class ComplexPassword : IPassword;

#endregion