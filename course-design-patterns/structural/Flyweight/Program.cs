using Flyweight;

GlyphFactory factory = new GlyphFactory();
DocumentEditor editor = new DocumentEditor(factory);

string text = "Hello World!";
int x = 10;
int y = 10;

foreach (var character in text)
{
    editor.InsertCharacter(character, "Ariel", x, y, 12, "black");
    x += 10;
}

string upperCaseText = text.ToUpper();

foreach (var character in text)
{
    editor.InsertCharacter(character, "Times New Roman", x, y, 14, "blue");
    x += 12;
}

editor.Render();