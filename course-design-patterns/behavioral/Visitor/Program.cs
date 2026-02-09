using Visitor;

List<IDocumentElement> elements =
[
    new TitleElement("The visitor design pattern"),
    new SubtitleElement("Intent"),
    new ContentElement("content")
];

Console.WriteLine("Text format:");
TextDocumentVisitor textDocumentVisitor = new();

foreach (var element in elements)
{
    element.Accept(textDocumentVisitor);
}

Console.WriteLine("Markdown format:");
MarkdownDocumentVisitor markdownDocumentVisitor = new();

foreach (var element in elements)
{
    element.Accept(markdownDocumentVisitor);
}