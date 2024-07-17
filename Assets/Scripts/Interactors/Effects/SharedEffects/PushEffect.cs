using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : InteractorEffect
{
    [SerializeField] private float force;
    public override void HotHitEffect(Collision collision)
    {
        HotHitEffect(collision.collider);
    }

    public override void HotHitEffect(Collider other)
    {
        CharacterPushability ability = other.GetComponent<CharacterPushability>();
        if (ability == null)
        {
            return;
        }
        Vector3 direction;
        if (Interactor is Projectile projectile)
        {
            direction = projectile.GetDirection();
        }
        else
        {
            direction = (other.transform.position - Interactor.transform.position).normalized;
        }
        direction.y = 0;
        ability.ApplyPush(direction, force);
    }
    
}
