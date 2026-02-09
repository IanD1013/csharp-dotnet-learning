using Bridge.ClothingItems;

namespace Bridge.ClothingFormatters;

public class ShoeClothingFormatter(Shoe shoe) : IClothingFormatter
{
    public string FormatDescription()
    {
        return $"A {shoe.Color} {(shoe.IsAthletic ? "athletic" : "casual")} shoe made of {shoe.Material}";
    }

    public Uri FormatImageUrl()
    {
        return new Uri($"images/shoe_{shoe.Brand}_{shoe.Color}.jpg", UriKind.Relative);
    }

    public string FormatTitle()
    {
        return $"{shoe.Brand} Shoe - Size {shoe.Size}";
    }
}
