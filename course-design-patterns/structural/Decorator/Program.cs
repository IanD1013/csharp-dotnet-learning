using Decorator;

var car = new LongRangeTeslaDecorator(
    car: new BasicTeslaModel3());

Console.WriteLine($"Description: {car.GetDescription()}");
Console.WriteLine($"Price: {car.GetPrice()}");
Console.WriteLine($"Range: {car.GetRange()}");