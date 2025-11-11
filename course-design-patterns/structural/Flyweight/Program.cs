using Flyweight;

FlyweightFactory factory = new FlyweightFactory();
Client client = new Client(factory);

client.Operation("Hello");
client.Operation("Hi");
client.Operation("World");