class LegacyRectangle(int left, int top, int right, int bottom)
{
    public int CalculateArea()
    {
        return (bottom - top) * (right - left);
    }
    
    public int CalculatePerimeter()
    {
        return 2 * (bottom - top) + 2 * (right - left);
    }

    public void Shift(int horizontal, int vertical)
    {
        top += vertical;
        bottom += vertical;

        right += horizontal;
        left += horizontal;
    }
}