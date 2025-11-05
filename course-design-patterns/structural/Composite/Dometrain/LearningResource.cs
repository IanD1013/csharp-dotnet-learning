namespace Composite.Dometrain;

public abstract class LearningResource
{
    public abstract decimal GetPrice();
    public abstract TimeSpan GetDuration();
    public abstract string GetName();
     
    public virtual void Add(LearningResource learningResource) {}
    public virtual void Remove(LearningResource learningResource) {}
    public virtual LearningResource? GetLearningResource(string nameParam)
    {
        return null;
    }
}