namespace Bridge.ClothingItems;

public record TankTop(
    string Size,
    string Color,
    string Material,
    bool IsRacerback = false,
    string Pattern = "Solid");