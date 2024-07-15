using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
public class BounceEffect : InteractorEffect
{
    [SerializeField] private int maxBounces = 3;
    private Rigidbody _projectileRigidBody;
    private Projectile _projectile;

    public override void Initialize(Interactor interactor)
    {
        base.Initialize(interactor);
        _projectileRigidBody = Interactor.GetComponent<Rigidbody>();
        _projectile = (Projectile) interactor;
    }

    public override void ColdHitEffect(Collision collision)
    {
        Vector3 incomingVector = _projectile.GetDirection();
        Vector3 normalVector = collision.contacts[0].normal;
        Vector3 newDirection = Vector3.Reflect(incomingVector, normalVector);
        _projectileRigidBody.position = collision.contacts[0].point + collision.contacts[0].normal * _projectile.GetColliderSize();
        _projectile.SetDirection(newDirection.normalized);
    }

    public override void FireEffect()
    {
        _projectile.SetColdLives(maxBounces + 1);
    }
}