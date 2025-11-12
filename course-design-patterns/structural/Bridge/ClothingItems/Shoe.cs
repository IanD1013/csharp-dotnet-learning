namespace Bridge.ClothingItems;

public record Shoe(
    string Brand,
    double Size,
    string Color,
    bool IsAthletic,
    string Material = "Leather");