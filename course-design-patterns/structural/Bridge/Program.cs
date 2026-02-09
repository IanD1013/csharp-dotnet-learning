// client code
using Bridge.ClothingFormatters;
using Bridge.ClothingItems;
using Bridge.ClothingViews;

// Create clothing items
Shoe nike = new(
    Brand: "Nike",
    Size: 10.5,
    Color: "Blue",
    IsAthletic: true,
    Material: "Mesh");

PoloShirt lacoste = new(
    Size: "M",
    Color: "Green",
    Material: "Cotton",
    HasPocket: true,
    Brand: "Lacoste");

Dress cocktail = new(
    Size: "S",
    Color: "Black",
    Style: "Cocktail",
    Material: "Silk");

Jeans levis = new(
    WaistSize: "32",
    Length: 34,
    Wash: "Dark",
    Fit: "Slim",
    HasStretch: true);

Shorts summer = new(
    Size: "L",
    Color: "Khaki",
    InseamLength: 7.5,
    Material: "Cotton",
    HasDrawstring: true);

TankTop sportTop = new(
    Size: "S",
    Color: "White",
    Material: "Polyester",
    IsRacerback: true,
    Pattern: "Striped");

// Create formatters for each item
IClothingFormatter shoeFormatter = new ShoeClothingFormatter(nike);
IClothingFormatter poloFormatter = new PoloShirtClothingFormatter(lacoste);
IClothingFormatter dressFormatter = new DressClothingFormatter(cocktail);
IClothingFormatter jeansFormatter = new JeansClothingFormatter(levis);
IClothingFormatter shortsFormatter = new ShortsClothingFormatter(summer);
IClothingFormatter tankTopFormatter = new TankTopClothingFormatter(sportTop);

// Create different views for each item
CompactClothingView compactShoeView = new(shoeFormatter);
CompactClothingView compactPoloView = new(poloFormatter);
MiniClothingView miniDressView = new(dressFormatter);
DetailedClothingView detailedJeansView = new(jeansFormatter);
CompactClothingView compactShortsView = new(shortsFormatter);
MiniClothingView miniTankTopView = new(tankTopFormatter);

// Display the views
Console.WriteLine("Detailed View of Shoe:");
compactShoeView.Display();
Console.WriteLine();

Console.WriteLine("Compact View of Polo Shirt:");
compactPoloView.Display();
Console.WriteLine();

Console.WriteLine("Mini View of Dress:");
miniDressView.Display();
Console.WriteLine();

Console.WriteLine("Detailed View of Jeans:");
detailedJeansView.Display();
Console.WriteLine();

Console.WriteLine("Compact View of Shorts:");
compactShortsView.Display();
Console.WriteLine();

Console.WriteLine("Mini View of Tank Top:");
miniTankTopView.Display();