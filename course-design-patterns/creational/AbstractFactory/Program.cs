using AbstractFactory.Factories;
using AbstractFactory.Products;

// Using the abstract factory pattern
var abstractFactory = new ConcreteFactory();

var product1 = abstractFactory.CreateProduct1();
var product2 = abstractFactory.CreateProduct2();

// Not using the abstract factory pattern
ConcreteProduct1 concreteProduct1 = new ConcreteProduct1();
ConcreteProduct2 concreteProduct2 = new ConcreteProduct2();