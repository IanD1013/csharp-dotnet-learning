using Consumer.ConsoleApp;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();

var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetRequiredService<IConsoleWriter>();

service.WriteLine("Hello from DI");