using FactoryMethod.FactoryMethod.Enemies;

namespace FactoryMethod.FactoryMethod.Levels;

public class CaveLevel : Level
{
    public override IEnemy CreateEnemy()
    {
        return new Goblin();
    }
}