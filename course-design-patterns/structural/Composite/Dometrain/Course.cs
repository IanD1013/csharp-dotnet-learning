namespace Composite.Dometrain;

public class Course(
    string name, 
    TimeSpan duration, 
    decimal price) : LearningResource
{
    public override decimal GetPrice()
    {
        return price;       
    }

    public override TimeSpan GetDuration()
    {
        return duration;      
    }

    public override string GetName()
    {
        return name;
    }
}