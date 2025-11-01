namespace Prototype;

public class Color(ushort red, ushort green, ushort blue) : IPrototype<Color>
{
    public static readonly Color LightGrey = new Color(217, 217, 217);
    
    public Color Clone()
    {
        return new Color(red, green, blue);
    }
}