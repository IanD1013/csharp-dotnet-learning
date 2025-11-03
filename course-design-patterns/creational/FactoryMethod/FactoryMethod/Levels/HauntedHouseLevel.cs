using FactoryMethod.FactoryMethod.Enemies;

namespace FactoryMethod.FactoryMethod.Levels;

public class HauntedHouseLevel : Level
{
    public override IEnemy CreateEnemy()
    {
        return new Ghost();
    }
}