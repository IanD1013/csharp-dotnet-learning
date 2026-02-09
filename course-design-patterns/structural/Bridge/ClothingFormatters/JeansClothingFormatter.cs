using Bridge.ClothingItems;

namespace Bridge.ClothingFormatters;

public class JeansClothingFormatter(Jeans jeans) : IClothingFormatter
{
    public string FormatDescription()
    {
        return $"{jeans.Wash} wash jeans with {jeans.Length}\" length";
    }

    public Uri FormatImageUrl()
    {
        return new Uri($"images/jeans_{jeans.Wash}.jpg", UriKind.Relative);
    }

    public string FormatTitle()
    {
        return $"Jeans - Size {jeans.WaistSize}W/{jeans.Length}L";
    }
}
