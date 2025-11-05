namespace Builder.FluentBuilder;

// fluent naming: BuildName -> SetName
// chain method one to another
public record Product(string Name, string Description)
{
    public class Builder
    {
        private string _name = string.Empty;
        private string _description = string.Empty;

        public Builder SetName(string name)
        {
            _name = name;
            return this;
        }

        public Builder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public Product Build()
        {
            return new Product(_name, _description);
        }
    }
}