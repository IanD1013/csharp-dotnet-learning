using Command;

CLI cli = new();
FileSystemReceiver fileSystemReceiver = new();

cli.RegisterCommand("ls", new ListCommand(fileSystemReceiver));
cli.RegisterCommand("cd", new ChangeDirectoryCommand(fileSystemReceiver));
cli.RegisterCommand("mkdir", new MakeDirectoryCommand(fileSystemReceiver));

cli.ExecuteCommand(commandName: "ls", args: []);
cli.ExecuteCommand(commandName: "mkdir", args: ["newFolder"]);
cli.ExecuteCommand(commandName: "cd", args: ["newFolder"]);
cli.ExecuteCommand(commandName: "123", args: ["newFolder"]);
