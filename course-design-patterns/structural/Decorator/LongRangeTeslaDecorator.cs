namespace Decorator;

public class LongRangeTeslaDecorator(ITeslaModel3 car) : TeslaDecorator(car)    
{       
    public override decimal GetPrice()
    {
        return base.GetPrice() + 3500;
    }

    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Long Range";      
    }
    
    public override int GetRange()
    {
        return base.GetRange() + 91;      
    }  
}