namespace State;

public interface ICharacterState
{
    void HandleUpdate(Character character);
    void HandleDamage(Character character, int amount);
    void HandlePowerUpCollected(Character character);
}