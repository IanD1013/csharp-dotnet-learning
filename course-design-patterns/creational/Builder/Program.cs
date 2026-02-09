using Builder.FluentBuilder;
using InnerBuilderPizza = Builder.InnerBuilder.Pizza;
using StepBuilderPizza = Builder.StepBuilder.Pizza;

#region FluentBuilder

Product product = new Product.Builder()
    .SetName("Simple product")
    .SetDescription("This is a simple product")
    .Build();

Console.WriteLine(product);

#endregion

#region InnerBuilder

InnerBuilderPizza pizza = new InnerBuilderPizza.Builder()
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

#region StepBuilder

// 1. Identify the distinct steps
// 2. Define each step as an interface
// 3. Add a method (one or more) that moves you to the next step
// 4. Have the last step build the underlying product
// 5. Implement the interfaces in the builder
// 6. Add an entry point in the builder that returns the first step
StepBuilderPizza pizza2 = StepBuilderPizza.Builder.Start()
    .SetDough(dough => dough 
        .SetThickness(3)
        .SetFlour("whole wheat"))
    .SetSauce("Spicy tomato sauce")
    .SetCheese("Vegan cheese")
    .AddTopping("Olives")
    .AddTopping("Onions")
    .Build();

#endregion