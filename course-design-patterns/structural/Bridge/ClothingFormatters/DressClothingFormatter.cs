using Bridge.ClothingItems;

namespace Bridge.ClothingFormatters;

public class DressClothingFormatter(Dress dress) : IClothingFormatter
{
    public string FormatDescription()
    {
        return $"A {dress.Color} {dress.Style} dress";
    }

    public Uri FormatImageUrl()
    {
        return new Uri($"images/dress_{dress.Style}_{dress.Color}.jpg", UriKind.Relative);
    }

    public string FormatTitle()
    {
        return $"{dress.Style} Dress - Size {dress.Size}";
    }
}
