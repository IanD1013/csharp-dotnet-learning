namespace Composite.Dometrain;

public class Bundle(string name) : LearningResource
{
    private readonly List<LearningResource> _children = [];
    
    public override decimal GetPrice()
    {
        return _children.Sum(x => x.GetPrice()) * 0.8m;   
    }

    public override TimeSpan GetDuration()
    {
        return new TimeSpan(_children.Sum(x => x.GetDuration().Ticks));
    }

    public override string GetName()
    {
        return name;   
    }
    
    public override void Add(LearningResource learningResource)
    {
        _children.Add(learningResource);
    }
    
    public override void Remove(LearningResource learningResource)
    {
        _children.Remove(learningResource);
    }

    public override LearningResource? GetLearningResource(string nameParam)
    {
        return _children.SingleOrDefault(x => x.GetName() == nameParam);  
    }
}