namespace Flyweight;

public class Glyph : IGlyph
{
    private readonly char _character;
    private readonly string _fontFamily;

    public Glyph(char character, string fontFamily)
    {
        _fontFamily = fontFamily;
        _character = character;

        Console.WriteLine($"Creating new glyph '{_character}' from font '{_fontFamily}'");
    }

    public void Render(int x, int y, int size, string color)
    {
        Console.WriteLine($"Rendering glyph '{_character}' at {x}, {y} with size {size} and color {color}");
    }
}