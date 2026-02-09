namespace Builder.InnerBuilder;

public record Pizza(
    Dough Dough,
    string Sauce,
    string Cheese,
    List<string> Toppings)
{
    public class Builder
    {
        private Dough _dough = null!;
        private string _sauce = string.Empty;
        private string _cheese = string.Empty;
        private List<string> _toppings = [];
        
        public Builder SetDough(Action<Dough.Builder> buildDoughAction)
        {
            var doughBuilder = new Dough.Builder();
            buildDoughAction(doughBuilder); 
            _dough = doughBuilder.Build();
            
            return this;
        }
        
        public Builder SetSauce(string sauce)
        {
            _sauce = sauce;
            return this;
        }

        public Builder SetCheese(string cheese)
        {
            _cheese = cheese;
            return this;
        }
        
        public Builder AddTopping(string topping)
        {
            _toppings.Add(topping);
            return this;
        }
        
        public Pizza Build()
        {
            return new Pizza(_dough, _sauce, _cheese, _toppings);
        }
    }    
}