namespace Interpreter.SearchQueryExample;

public class ContainsExpression(string str): Expression
{
    private readonly string _str = str;
    
    public override List<string> Interpret(List<string> words)
    {
        return words
            .Where(word => word.Contains(_str, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}