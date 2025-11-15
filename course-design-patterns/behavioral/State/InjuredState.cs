namespace State;

public class InjuredState : ICharacterState
{
    public void HandleUpdate(Character character)
    {
        character.ModifyHealth(2);

        if (character.Health >= 30)
        {
            character.SetState(new NormalState());
        }
    }

    public void HandleDamage(Character character, int amount)
    {
        character.ModifyHealth(-(amount * 2));
    }

    public void HandlePowerUpCollected(Character character)
    {
        character.ModifyHealth(50);
        character.SetState(new NormalState());
    }
}