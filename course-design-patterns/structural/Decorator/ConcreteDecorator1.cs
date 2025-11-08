namespace Decorator;

public class ConcreteDecorator1(Component component) : Decorator(component)     
{
    public override void Operation()
    {
        Console.WriteLine("----- ConcreteDecorator 1  -----");
        _component.Operation();
        Console.WriteLine("----- ConcreteDecorator 1  -----");
    }   
}