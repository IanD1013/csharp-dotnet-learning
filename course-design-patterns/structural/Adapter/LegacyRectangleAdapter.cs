class LegacyRectangleAdapter(LegacyRectangle legacyRectangle) : IRectangle
{
    public long GetArea()
    {
        return legacyRectangle.CalculateArea();
    }

    public long GetPerimeter()
    {
        return legacyRectangle.CalculatePerimeter();   
    }

    public void Move(long dx, long dy)
    {
        legacyRectangle.Shift(Convert.ToInt32(dx), Convert.ToInt32(dy));
    }
}