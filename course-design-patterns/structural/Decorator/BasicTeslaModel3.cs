namespace Decorator;

class BasicTeslaModel3 : ITeslaModel3
{
    public string GetDescription()
    {
        return "Tesla Model 3 Rear-Wheel Drive";
    }

    public int GetRange()
    {
        return 272;
    }

    public decimal GetPrice()
    {
        return 38990m;
    }
}