using Strategy;

ShoppingCart shoppingCart = new();

shoppingCart.SetStrategy(new CreditCardPaymentStrategy());
shoppingCart.Checkout(10);