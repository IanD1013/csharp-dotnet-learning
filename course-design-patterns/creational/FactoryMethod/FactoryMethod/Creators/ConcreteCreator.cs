using FactoryMethod.FactoryMethod.Products;

namespace FactoryMethod.FactoryMethod.Creators;

public class ConcreteCreator : Creator
{
    public override Product CreateProduct()
    {
        return new ConcreteProduct();
    }
}