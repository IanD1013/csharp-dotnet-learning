namespace Interpreter.SearchQueryExample;

public class NotExpression(string word): Expression
{
    private readonly string _word = word;
    
    public override List<string> Interpret(List<string> words)
    {
        return words
            .Where(word => !word.Equals(_word, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}