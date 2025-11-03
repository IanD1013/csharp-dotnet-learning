using AbstractFactory.Common;

namespace AbstractFactory.CaveLevel;

public class CaveLevelElementFactory : LevelElementFactory
{
    protected override IEnemy CreateEnemy()
    {
        return new Goblin();
    }

    protected override IWeapon CreateWeapon()
    {
        return new Crystal();
    }

    protected override IPowerUp CreatePowerUp()
    {
        return new Axe();
    }
}