/*
 * The Factory Family:
 * 1. Simple Factory Pattern
 * 2. Factory Method Pattern: define an interface for creating an object, but let subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses.
 * 3. Abstract Factory Pattern
 */

using FactoryMethod.FactoryMethod.Creators;
using FactoryMethod.FactoryMethod.Products;

// Using the Factory Method Pattern
Creator creator = new ConcreteCreator();
Product product = creator.CreateProduct();

// Not using the Factory Method Pattern
ConcreteProduct concreteProduct = new ConcreteProduct();

// our aim is to give client the product without having to rely on any specific concrete product
// this also gives us flexibility to use different creators
// the client can focus on only and solely the behavior that he wants -> invoke some methods on the product 