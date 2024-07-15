using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AutoGuidedEffect : ProjectileEffect
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private LayerMask detectionLayerMask; 
    [SerializeField] private float followDuration = 1f; 
    [SerializeField]private float activationDelay = 0.5f;
    private Transform _target;
    private float _activationTimer;
    private bool _isFollowing;
    private float _followTimer;

    public override void Initialize(Projectile projectile)
    {
        base.Initialize(projectile);
        _activationTimer = 0f;
        _isFollowing = false;
        _followTimer = 0f;
    }
    public override void UpdateEffect()
    {
        if (!_isFollowing)
        {
            _activationTimer += Time.deltaTime;
            if (_activationTimer >= activationDelay)
            {
                _isFollowing = true;
            }
            return;
        }

        if (_target == null)
        {
            FindTarget();
        }

        if (_target != null && _followTimer < followDuration)
        {
            RotateTowardsTarget();
            _followTimer += Time.deltaTime;
        }
    }

    private void FindTarget()
    {
        ExplosionDebugManager.RegisterExplosion(Projectile.transform.position, detectionRadius, Color.green);
        Collider[] hitColliders = Physics.OverlapSphere(Projectile.transform.position, detectionRadius, detectionLayerMask);
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            Health health = hitCollider.GetComponent<Health>();
            if (health != null)
            {
                float distance = Vector3.Distance(Projectile.transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = health.transform;
                }
            }
        }

        if (closestTarget != null)
        {
            _target = closestTarget;
        }
    }


    private void RotateTowardsTarget()
    {
        Vector3 direction = (_target.position - Projectile.transform.position).normalized;
        Projectile.SetDirection(Vector3.Slerp(Projectile.GetDirection(), direction, rotationSpeed * Time.deltaTime));
    }

    public override void ResetEffect()
    {
        _target = null;
        _activationTimer = 0f;
        _isFollowing = false;
        _followTimer = 0f;
    }
}
