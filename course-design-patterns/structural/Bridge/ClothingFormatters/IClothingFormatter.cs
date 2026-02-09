namespace Bridge.ClothingFormatters;

public interface IClothingFormatter
{
    Uri FormatImageUrl();
    string FormatTitle();
    string FormatDescription();
}