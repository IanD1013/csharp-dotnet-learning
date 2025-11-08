class Adapter(Adaptee adaptee) : ITarget
{
    public void Request()
    {
        adaptee.SpecificRequest();
    }
}