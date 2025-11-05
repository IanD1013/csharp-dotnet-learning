using Builder.FluentBuilder;

Product product = new Product.Builder()
    .SetName("Simple product")
    .SetDescription("This is a simple product")
    .Build();

Console.WriteLine(product);