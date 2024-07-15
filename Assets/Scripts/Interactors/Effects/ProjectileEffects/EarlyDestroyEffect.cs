using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Projectile))]
public class EarlyDestroyEffect : InteractorEffect
{
    [SerializeField] private bool destroyOnColdHit = false;

    [SerializeField] private bool destroyOnHotHit = false;

    public override void ColdHitEffect(Collision collision)
    {
        if (!destroyOnColdHit)
        {
            return;
        }
        Interactor.DestroyInteractor();
    }

    public override void HotHitEffect(Collision collision)
    {
        if (!destroyOnHotHit)
        {
            return;
        }
        Interactor.DestroyInteractor();
    }
}
