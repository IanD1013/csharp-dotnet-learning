namespace Decorator;

public abstract class Decorator(Component component) : Component 
{
    protected Component _component = component;
    
    public virtual void Operation()
    {
        _component.Operation();
    }
}