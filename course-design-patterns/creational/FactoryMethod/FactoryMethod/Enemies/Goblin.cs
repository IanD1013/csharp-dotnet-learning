namespace FactoryMethod.FactoryMethod.Enemies;

public class Goblin : IEnemy
{
    public void Scream()
    {
        Console.WriteLine("Goblin screams");
    }

    public void Attack()
    {
        Console.WriteLine("Goblin attacks");
    }
}