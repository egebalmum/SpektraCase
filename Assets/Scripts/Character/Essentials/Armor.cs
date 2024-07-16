using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Collections;
using UnityEngine;

public class Armor : CharacterAbility
{
    [SerializeField] private float armorPoint = 100;
    [SerializeField] private float _currentArmor;
    public override void Initialize()
    {
        _currentArmor = armorPoint;
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
        _currentArmor = armorPoint;
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
