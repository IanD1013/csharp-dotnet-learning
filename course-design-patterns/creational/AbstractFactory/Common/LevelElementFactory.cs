namespace AbstractFactory.Common;

public abstract class LevelElementFactory
{
    protected abstract IEnemy CreateEnemy();
    protected abstract IWeapon CreateWeapon();
    protected abstract IPowerUp CreatePowerUp();

    public void SetupEnvironment()
    {
        var enemy = CreateEnemy();
        var weapon = CreateWeapon();
        var powerUp = CreatePowerUp();
    }
}