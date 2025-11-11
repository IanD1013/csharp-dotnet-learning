namespace Flyweight;

public class GlyphFactory
{
    private readonly Dictionary<(char character, string fontFamily), IGlyph> _flyweights = [];
    
    public IGlyph GetGlyph(char character, string fontFamily) 
    {
        if (!_flyweights.ContainsKey((character, fontFamily)))      
        {
            _flyweights[(character, fontFamily)] = new Glyph(character, fontFamily);
        }
        
        return _flyweights[(character, fontFamily)];
    }
}