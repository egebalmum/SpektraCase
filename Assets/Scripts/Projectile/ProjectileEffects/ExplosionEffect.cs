using System.Collections;
using UnityEngine;

public class ExplosionEffect : ProjectileEffect
{
    [SerializeField] private float explosionRadius = 7f;
    [SerializeField] private float explosionDamage = 50;
    [SerializeField] private LayerMask explosionLayerMask;
    private bool _exploded = false;
    public override void HotHitEffect(Collision collision)
    {
        if (_exploded) return;
        _exploded = true;
        Projectile.DestroyProjectile();
    }

    public override void DestroyEffect()
    {
        ExplosionDebugManager.RegisterExplosion(Projectile.transform.position, explosionRadius, Color.red);
        Collider[] colliders = Physics.OverlapSphere(Projectile.transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider collider in colliders)
        {
            Health health = collider.GetComponent<Health>();
            if (health != null)
            {
                if (HasLineOfSight(collider.transform))
                {
                    health.InstantDamage(explosionDamage);
                }
            }
        }
    }

    private bool HasLineOfSight(Transform target)
    {
        RaycastHit hit;
        Vector3 direction = (target.position - Projectile.transform.position).normalized;
        if (Physics.Raycast(Projectile.transform.position, direction, out hit, explosionRadius, explosionLayerMask))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }

    public override void ResetEffect()
    {
        _exploded = false;
    }
}
