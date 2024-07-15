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
        Vector3 direction;
        if (Interactor is Projectile projectile)
        {
            direction = projectile.GetDirection();
        }
        else
        {
            direction = other.transform.position - Interactor.transform.position;
        }
        direction.y = 0;
        CharacterPushability ability = other.GetComponent<CharacterPushability>();
        if (ability != null)
        {
            ability.ApplyPush(direction, force);
        }
    }
}
