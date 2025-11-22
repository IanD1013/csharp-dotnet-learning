using Command;

Receiver receiver = new();
Command.Command command1 = new ConcreteCommand(receiver, "1");
Command.Command command2 = new ConcreteCommand(receiver, "2");
Command.Command command3 = new ConcreteCommand(receiver, "3");

Invoker invoker = new();
invoker.AddCommand(command1);
invoker.AddCommand(command2);
invoker.AddCommand(command3);

invoker.ExecuteCommands();
invoker.UndoLastCommand();
invoker.UndoLastCommand();