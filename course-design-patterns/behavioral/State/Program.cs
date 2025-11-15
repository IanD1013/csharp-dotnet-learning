using State;

Character hero = new(); // normal state
Console.WriteLine($"Initial Health: {hero.Health}");

hero.TakeDamage(30);
Console.WriteLine($"After damage: {hero.Health}");

hero.CollectPowerUp();
Console.WriteLine($"After power up: {hero.Health}, has power up: {hero.HasPowerUp}");

hero.TakeDamage(30);
Console.WriteLine($"After second damage: {hero.Health}");

for (var i = 0; i < 12; i++)
{
    hero.Update();
}

Console.WriteLine($"After 12 ticks: {hero.Health}, has power up: {hero.HasPowerUp}");

hero.TakeDamage(70);
Console.WriteLine($"After third damage: {hero.Health}");