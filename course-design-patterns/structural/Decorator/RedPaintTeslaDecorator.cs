namespace Decorator;

public class RedPaintTeslaDecorator(ITeslaModel3 car) : TeslaDecorator(car)     
{
    public override decimal GetPrice()
    {
        return base.GetPrice() + 2000;
    }

    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Red Paint";
    }
}