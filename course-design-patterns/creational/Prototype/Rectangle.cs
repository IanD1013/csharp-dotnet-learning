namespace Prototype;

public class Rectangle(int width, int height, Color color) : IShape
{
    public IShape Clone()
    {
        return new Rectangle(width, height, color.Clone());
    }
}