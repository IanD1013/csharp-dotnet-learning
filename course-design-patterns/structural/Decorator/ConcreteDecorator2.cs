namespace Decorator;

public class ConcreteDecorator2(Component component) : Decorator(component)     
{
    public override void Operation()
    {
        Console.WriteLine("----- ConcreteDecorator 2  -----");
        _component.Operation();
        Console.WriteLine("----- ConcreteDecorator 2  -----");
    }   
}