using AbstractFactory.Products;

namespace AbstractFactory.Factories;

public abstract class AbstractFactory
{
    public abstract Product1 CreateProduct1();
    public abstract Product2 CreateProduct2();
}