using System;
using UnityEngine;

public class Armor : CharacterAbility
{
    [SerializeField] private float startArmorPoint = 100;
    private float _currentArmor;
    public Action<float> OnArmorChange;
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
       if (_currentArmor < 0)
       {
           _currentArmor = 0;
       }
       OnArmorChange?.Invoke(_currentArmor/startArmorPoint);
    }

    public float GetArmorPoint()
    {
        return _currentArmor;
    }

    public override void ResetAbility()
    {
        _currentArmor = startArmorPoint;
        OnArmorChange?.Invoke(_currentArmor/startArmorPoint);
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
