using FactoryMethod.FactoryMethod.Products;

namespace FactoryMethod.FactoryMethod.Creators;

public abstract class Creator
{
    public abstract Product CreateProduct();
}