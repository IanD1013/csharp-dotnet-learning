namespace Builder.Classic.Builders;

public interface IBuilder
{
    void BuildName();
    
    void BuildDescription();
    
    Product Build();
}