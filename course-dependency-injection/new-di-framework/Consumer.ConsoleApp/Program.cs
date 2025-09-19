using Consumer.ConsoleApp;
using Vax;

var services = new ServiceCollection();

services.AddSingleton<IConsoleWriter, ConsoleWriter>();
services.AddSingleton<IIdGenerator, IdGenerator>();

var serviceProvider = services.BuildServiceProvider();

var service1 = serviceProvider.GetService<IIdGenerator>();
var service2 = serviceProvider.GetService<IIdGenerator>();

service1?.PrintId();
service2?.PrintId();