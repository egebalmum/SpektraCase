using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Upgrade/WeaponUpgrade/Scope")]
public class ScopeUpgrade : WeaponUpgrade
{
    [SerializeField] private float scopeMultiplier;
    private float _oldRange;
    public override void AddUpgrade(Weapon weapon)
    {
        _oldRange = weapon.range;
        weapon.range = _oldRange * scopeMultiplier;
    }

    public override void RemoveUpgrade(Weapon weapon)
    {
        weapon.range = _oldRange;
    }
}
