using Builder.Classic;
using Builder.Classic.Builders;

IBuilder builder = new SimpleProductBuilder();

builder.BuildName();
builder.BuildDescription();

Product product = builder.Build();

Console.WriteLine(product);