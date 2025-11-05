using Builder.NestedBuilder;

Product.Builder builder = new();

builder.BuildName("Simple product");
builder.BuildDescription("This is a simple product");

var product = builder.Build();

Console.WriteLine(product);