using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Armor : CharacterAbility
{
    [SerializeField] private float startArmorPoint = 100;
    private float _currentArmor;
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
