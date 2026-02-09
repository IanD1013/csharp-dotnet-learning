namespace Builder.InnerBuilder;

public record Dough(int Thickness, string Flour)
{
    public class Builder
    {
        private int _thickness;
        private string _flour; 
        
        public Builder SetThickness(int thickness)
        {
            _thickness = thickness;
            return this;
        }
        
        public Builder SetFlour(string flour)
        {
            _flour = flour;
            return this;
        }

        public Dough Build()
        {
            return new Dough(_thickness, _flour);
        }
    }
}