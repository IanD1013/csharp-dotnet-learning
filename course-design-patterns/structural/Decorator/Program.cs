using Decorator;

Component component = new ConcreteDecorator1(new ConcreteDecorator1(new ConcreteComponent()));
component.Operation();