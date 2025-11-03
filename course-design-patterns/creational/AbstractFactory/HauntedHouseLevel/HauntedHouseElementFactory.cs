using AbstractFactory.Common;

namespace AbstractFactory.HauntedHouseLevel;

public class HauntedHouseElementFactory : LevelElementFactory
{
    protected override IEnemy CreateEnemy()
    {
        return new Ghost();
    }

    protected override IWeapon CreateWeapon()
    {
        return new Orb();
    }

    protected override IPowerUp CreatePowerUp()
    {
        return new Staff();
    }
}