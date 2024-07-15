using System;
using UnityEngine;

[Serializable]
public abstract class ProjectileEffect : MonoBehaviour
{
    protected Projectile Projectile;

    public virtual void Initialize(Projectile projectile)
    {
        Projectile = projectile;
    }
    public virtual void FireEffect() { }

    public virtual void HotHitEffect(Collision collision) { }

    public virtual void ColdHitEffect(Collision collision) { }
    public virtual void UpdateEffect() { }
    public virtual void DestroyEffect() { }
    public virtual void ResetEffect() { }
}