using UnityEngine;

public class Armor : CharacterAbility
{
    [SerializeField] private float startArmorPoint = 100;
    [SerializeField] private float _currentArmor;
    public override void Initialize()
    {
        _currentArmor = startArmorPoint;
    }

    public void InstantDamage(float value)
    {
        if (!GetAbilityEnabled())
        {
            return;
        }
       _currentArmor -= value;
    }

    public float getArmorPoint()
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
