namespace Command;

public class FileSystemReceiver
{
    public void MakeDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }   
    
    public void ChangeDirectory(string path)
    {
        Directory.SetCurrentDirectory(path);
    }
    
    public void ListFiles(string path)
    {
        foreach (var file in Directory.GetFiles(path))
        {
            Console.WriteLine(file);
        }
    }
}