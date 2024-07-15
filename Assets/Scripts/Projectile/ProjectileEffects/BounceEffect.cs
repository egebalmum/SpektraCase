using Unity.VisualScripting;
using UnityEngine;


public class BounceEffect : ProjectileEffect
{
    [SerializeField] private int maxBounces = 3;
    private Rigidbody _projectileRigidBody;

    public override void Initialize(Projectile projectile)
    {
        base.Initialize(projectile);
        _projectileRigidBody = Projectile.GetComponent<Rigidbody>();
    }

    public override void ColdHitEffect(Collision collision)
    {
        Vector3 incomingVector = Projectile.GetDirection();
        Vector3 normalVector = collision.contacts[0].normal;
        Vector3 newDirection = Vector3.Reflect(incomingVector, normalVector);
        _projectileRigidBody.position = collision.contacts[0].point + collision.contacts[0].normal * Projectile.GetColliderSize();
        Projectile.SetDirection(newDirection.normalized);
    }

    public override void FireEffect()
    {
        Projectile.SetColdLives(maxBounces + 1);
    }
}