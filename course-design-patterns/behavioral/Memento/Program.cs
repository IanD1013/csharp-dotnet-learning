using Memento;

TextBox textBox = new();
TextHistory textHistory = new();

textBox.SetText("Hello");
textHistory.Backup(textBox.Save());

textBox.SetText("Hello, World!");
textHistory.Backup(textBox.Save());

Console.WriteLine($"Current text: {textBox.GetText()}");

textHistory.Undo(textBox);
Console.WriteLine($"After undo: {textBox.GetText()}");

textHistory.Undo(textBox);
Console.WriteLine($"After second undo: {textBox.GetText()}");

textHistory.Redo(textBox);
Console.WriteLine($"After first redo: {textBox.GetText()}");