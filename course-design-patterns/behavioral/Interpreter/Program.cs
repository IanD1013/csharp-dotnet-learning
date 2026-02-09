using Interpreter;
using Interpreter.SearchQueryExample;

// var expression = new AddExpression(
//     left: new NumberExpression(1),
//     right: new MultiplyExpression(
//         left: new NumberExpression(2),
//         right: new NumberExpression(3)));
//
// var result = expression.Interpret();
//
// Console.WriteLine(result);




List<string> context = ["hello", "world", "elephant", "help"];

// var expression = new NotExpression(word: "hello");
var expression = new ContainsExpression(str: "el");
var result = expression.Interpret(context);
Console.WriteLine(string.Join(",", result));