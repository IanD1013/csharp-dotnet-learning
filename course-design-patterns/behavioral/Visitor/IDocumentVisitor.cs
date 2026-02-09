namespace Visitor;

public interface IDocumentVisitor
{
    void Visit(TitleElement element);
    void Visit(SubtitleElement element);
    void Visit(ContentElement element);
}