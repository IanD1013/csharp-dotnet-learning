using Bridge.ClothingItems;

namespace Bridge.ClothingFormatters;

public class PoloShirtClothingFormatter(PoloShirt poloShirt) : IClothingFormatter
{
    public string FormatDescription()
    {
        return $"A {poloShirt.Color} polo shirt made of {poloShirt.Material}";
    }

    public Uri FormatImageUrl()
    {
        return new Uri($"images/poloshirt_{poloShirt.Brand}_{poloShirt.Color}.jpg", UriKind.Relative);
    }

    public string FormatTitle()
    {
        return $"{poloShirt.Brand} Polo Shirt - Size {poloShirt.Size}";
    }
}
