using Bridge.ClothingItems;

namespace Bridge.ClothingFormatters;

public class ShortsClothingFormatter(Shorts shorts) : IClothingFormatter
{
    public string FormatDescription()
    {
        return $"{shorts.Color} shorts with {(shorts.HasDrawstring ? "a drawstring" : "no drawstring")}";
    }

    public Uri FormatImageUrl()
    {
        return new Uri($"images/shorts_{shorts.Color}.jpg", UriKind.Relative);
    }

    public string FormatTitle()
    {
        return $"Shorts - Size {shorts.Size}";
    }
}
