
using Bridge.ClothingItems;

namespace Bridge.ClothingFormatters;

public class TankTopClothingFormatter(TankTop tankTop) : IClothingFormatter
{
    public string FormatDescription()
    {
        return $"A {tankTop.Color} tank top made of {tankTop.Material}";
    }

    public Uri FormatImageUrl()
    {
        return new Uri($"images/tanktop_{tankTop.Color}.jpg", UriKind.Relative);
    }

    public string FormatTitle()
    {
        return $"Tank Top - Size {tankTop.Size}";
    }
}