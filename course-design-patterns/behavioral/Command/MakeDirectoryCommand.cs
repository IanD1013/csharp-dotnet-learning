namespace Command;

public class MakeDirectoryCommand(FileSystemReceiver receiver) : ICommand
{
    private readonly FileSystemReceiver _receiver = receiver;

    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: mkdir <path>");
            return;
        }
        
        _receiver.MakeDirectory(path: args[0]);
    }
}