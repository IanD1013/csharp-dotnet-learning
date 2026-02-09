namespace Template_Method;

public class CsvParser : FileParser
{
    protected override Dictionary<string, string> ParseContent(string content)
    {
        Dictionary<string, string> result = [];

        foreach (var row in content.Split('\n'))
        {
            var parts = row.Split(',');
            result[parts[0]] = parts[1];
        }
        
        return result;
    }
}