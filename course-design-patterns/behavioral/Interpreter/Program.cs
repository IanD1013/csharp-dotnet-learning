using Interpreter;

var expression = new AddExpression(
    left: new NumberExpression(1),
    right: new MultiplyExpression(
        left: new NumberExpression(2),
        right: new NumberExpression(3)));

var result = expression.Interpret();

Console.WriteLine(result);