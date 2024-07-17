using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/WeaponUpgrade/Barrel")]
public class BarrelUpgrade : WeaponUpgrade
{
    [SerializeField] private float damageMultiplier;
    private List<(Interactor, float)> oldDamages;
    public override void AddUpgrade(Weapon weapon)
    {
        oldDamages = new List<(Interactor, float)>();
        HandleInteractor(weapon.projectile);
    }

    public override void RemoveUpgrade(Weapon weapon)
    {
        foreach (var (interactor,damage) in oldDamages)
        {
            interactor.GetComponent<DamageEffect>().damage = damage;
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
        oldDamages.Add((interactor,effect.damage));
        effect.damage *= damageMultiplier;
    }
}
