namespace Decorator;

public abstract class TeslaDecorator(ITeslaModel3 car) : ITeslaModel3 
{
    protected ITeslaModel3 _car = car;
    
    public virtual decimal GetPrice()
    {           
        return _car.GetPrice();      
    }

    public virtual string GetDescription()
    {
        return _car.GetDescription();       
    }

    public virtual int GetRange()
    {
        return _car.GetRange();      
    }
}