namespace FactoryMethod.FactoryMethod.Enemies;

public class Ghost : IEnemy
{
    public void Scream()
    {
        Console.WriteLine("Ghost screams");
    }

    public void Attack()
    {
        Console.WriteLine("Ghost attacks");   
    }
}