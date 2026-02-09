using Bridge.ClothingFormatters;

namespace Bridge.ClothingViews;

public abstract class ClothingView(IClothingFormatter clothingFormatter)
{
    protected IClothingFormatter ClothingFormatter { get; init; } = clothingFormatter;

    public abstract void Display();
}