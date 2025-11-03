using Builder.Classic;
using Builder.Classic.Builders;

IBuilder builder = new SimpleProductBuilder();

ProductDirector director = new ProductDirector(builder);

director.ConstructProduct();
Product product = builder.Build();

Console.WriteLine(product);