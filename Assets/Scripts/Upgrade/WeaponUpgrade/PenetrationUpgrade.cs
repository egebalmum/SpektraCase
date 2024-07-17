using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Upgrade/WeaponUpgrade/Penetration")]
public class PenetrationUpgrade : WeaponUpgrade
{
    [FormerlySerializedAs("damageMultiplier")] [SerializeField] private float penetrationMultiplier;
    private List<(Interactor, float)> oldPenetrations;
    public override void AddUpgrade(Weapon weapon)
    {
        oldPenetrations = new List<(Interactor, float)>();
        HandleInteractor(weapon.projectile);
    }

    public override void RemoveUpgrade(Weapon weapon)
    {
        foreach (var (interactor,pen) in oldPenetrations)
        {
            interactor.GetComponent<DamageEffect>().armorPenetration = pen;
        }
    }
    private void HandleInteractor(Interactor interactor)
    {
        InteractorSpawnEffect[] spawnEffects = interactor.GetComponents<InteractorSpawnEffect>();
        if (spawnEffects != null)
        {
            foreach (var spawnEffect in spawnEffects)
            {
                HandleInteractor(spawnEffect.interactor); 
            }
        }
        DamageEffect effect = interactor.GetComponent<DamageEffect>();
        if (effect == null)
        {
            return;
        }
        oldPenetrations.Add((interactor,effect.armorPenetration));
        effect.armorPenetration *= penetrationMultiplier;
    }
}