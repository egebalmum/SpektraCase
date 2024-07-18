using UnityEngine;

public class Armor : CharacterAbility
{
    [SerializeField] private float startArmorPoint = 100;
    private float _currentArmor;
    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        _currentArmor = startArmorPoint;
    }

    public void ApplyDamageInstant(float value)
    {
        if (!GetAbilityEnabled())
        {
            return;
        }
       _currentArmor -= value;
    }

    public float GetArmorPoint()
    {
        return _currentArmor;
    }

    public override void ResetAbility()
    {
        _currentArmor = startArmorPoint;
    }

    public override void OnRespawn()
    {
        ResetAbility();
        SetAbilityEnabled(true);
    }

    public override void OnDeath()
    {
        SetAbilityEnabled(false);
    }
}
