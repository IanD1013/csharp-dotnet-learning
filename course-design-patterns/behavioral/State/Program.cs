using State;

Context context = new(new ConcreteStateA());

context.Request(); // State A
context.Request(); // State B
context.Request(); // State A