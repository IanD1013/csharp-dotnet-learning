using System.Globalization;

namespace Template_Method;

public abstract class FileParser
{
    public Dictionary<string, string> ParseFile(string path)
    {
        LogOperation("Validating the file");
        ValidateFile(path);
        
        LogOperation("Loading the file");
        var content = File.ReadAllText(path);
        
        LogOperation("Parsing the file");
        var data = ParseContent(content);
        
        LogOperation("Validating the file");
        EnrichData(data);
        
        LogOperation("Enriching the file");
        ValidateData(data);

        return data;
    }
    
    protected abstract Dictionary<string, string> ParseContent(string content);

    protected virtual void EnrichData(Dictionary<string, string> data)
    {
        data["parsedAt"] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
    }

    protected virtual void ValidateData(Dictionary<string, string> data)
    {
        
    }
    
    protected virtual void LogOperation(string message)
    {
        Console.WriteLine($"{DateTime.UtcNow:HH:mm:ss}: {message}");
    }
    
    private void ValidateFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new Exception("File does not exist");
        }

        if (new FileInfo(path).Length == 0)
        {
            throw new Exception("File is empty");
        }
    }
}