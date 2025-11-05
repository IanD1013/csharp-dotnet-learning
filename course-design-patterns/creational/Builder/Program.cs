using Builder.FluentBuilder;
using Builder.InnerBuilder;

#region FluentBuilder

Product product = new Product.Builder()
    .SetName("Simple product")
    .SetDescription("This is a simple product")
    .Build();

Console.WriteLine(product);

#endregion

#region InnerBuilder

Pizza pizza = new Pizza.Builder()
    .SetDough(dough => dough // param is of type Dough.Builder
        .SetThickness(3)
        .SetFlour("whole wheat"))
    .SetSauce("Spicy tomato sauce")
    .SetCheese("Vegan cheese")
    .AddTopping("Olives")
    .AddTopping("Onions")
    .Build();

Console.WriteLine(pizza);

#endregion