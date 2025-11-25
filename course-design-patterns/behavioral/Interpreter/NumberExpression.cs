namespace Interpreter;

public class NumberExpression(int number) : Expression
{
    private readonly int _number = number;
    
    public override int Interpret()
    {
        return _number;
    }
}