namespace Command;

public class Invoker
{
    private readonly List<Command> _commands = [];
    private readonly Stack<Command> _undoStack = [];

    public void AddCommand(Command command)
    {
        _commands.Add(command);
    }

    public void ExecuteCommands()
    {
        foreach (var command in _commands)
        {
            command.Execute();
            _undoStack.Push(command);
        }
        
        _commands.Clear();
    }  
    
    public void UndoLastCommand()
    {
        var command = _undoStack.Pop();
        command.Undo();
    }
}