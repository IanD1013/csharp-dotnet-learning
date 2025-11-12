namespace Bridge.ClothingItems;

public record Jeans(
    string WaistSize,
    int Length,
    string Wash,
    string Fit = "Regular",
    bool HasStretch = false);