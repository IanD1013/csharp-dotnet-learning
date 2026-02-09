namespace Builder.Classic.Builders;

public class ComplexProductBuilder : IBuilder
{
    private string _name = "";
    private string _description = "";
    public void BuildName()
    {
        _name = "Complex Product";
    }

    public void BuildDescription()
    {
        _description = "This is a complex product";   
    }

    public Product Build()
    {
        return new Product(Name: _name, Description: _description);
    }
}