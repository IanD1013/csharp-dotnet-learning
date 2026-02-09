namespace Strategy;

public class ShoppingCart
{
    private IPaymentStrategy? _strategy;
    
    public void SetStrategy(IPaymentStrategy paymentStrategy)
    {
        _strategy = paymentStrategy;
    }
    
    public void Checkout(decimal amount)        
    {
        _strategy?.Pay(amount);
    }
}