namespace Decorator;

public class BluePaintTeslaDecorator(ITeslaModel3 car) : TeslaDecorator(car)     
{
    public override decimal GetPrice()
    {
        return base.GetPrice() + 1000;
    }

    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Blue Paint";
    }
}