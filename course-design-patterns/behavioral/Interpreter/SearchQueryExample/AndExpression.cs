namespace Interpreter.SearchQueryExample;

public class AndExpression(Expression left, Expression right) : Expression
{
    private readonly Expression _left = left;
    private readonly Expression _right = right;
    
    public override List<string> Interpret(List<string> words)
    {
        return _left.Interpret(words)
            .Intersect(_right.Interpret(words))
            .ToList();
    }
}