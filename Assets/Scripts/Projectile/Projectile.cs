using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    [SerializeField] private bool coldHitAllowed;
    [SerializeField] private bool hotHitAllowed;
    private int _projectileLivesCold = 1;
    private int _projectileLivesHot = 1;
    private float _speed;
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private bool _isFired;
    private bool _isDestroyed;
    private float _remainingDistance;
    private List<ProjectileEffect> _effects;
    
    public void InitializeProjectile()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _effects = GetComponents<ProjectileEffect>().ToList();
        InitializeEffects();
    }
    private void InitializeEffects()
    {
        foreach (var effect in _effects)
        {
            effect.Initialize(this);
        }
    }
    private void FixedUpdate()
    {
        if (!_isFired)
        {
            return;
        }
        MoveProjectile();
        foreach (var effect in _effects)
        {
            effect.UpdateEffect();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        OnCollision(other);
    }

    public float GetColliderSize()
    {
        return _collider.bounds.size.x;
    }
    public Vector3 GetDirection()
    {
        return _direction;
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    public void SetColdLives(int value)
    {
        if (_projectileLivesCold < value)
        {
            _projectileLivesCold = value;
        }
    }

    public int GetColdLives()
    {
        return _projectileLivesCold;
    }

    public void SetHotLives(int value)
    {
        if (_projectileLivesHot < value)
        {
            _projectileLivesHot = value;
        }
    }

    public int GetHotLives()
    {
        return _projectileLivesHot;
    }
    
    public void ResetProjectile()
    {
        _projectileLivesCold = 1;
        _projectileLivesHot = 1;
        _isFired = false;
        
        if (_effects == null)
        {
            return;
        }
        foreach (var effect in _effects)
        {
            effect.ResetEffect();
        }
    }
    
    private void OnCollision(Collision other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health != null) //HOT HIT
        {
            if (!hotHitAllowed)
            {
                return;
            }
            foreach (var effect in _effects)
            {
                effect.HotHitEffect(other);
            }

            _projectileLivesHot -= 1;
        }
        else //COLD HIT
        {
            if (!coldHitAllowed)
            {
                return;
            }
            foreach (var effect in _effects)
            {
                effect.ColdHitEffect(other);
            }

            _projectileLivesCold -= 1;
        }
        if (_projectileLivesHot <= 0 || _projectileLivesCold <= 0)
        {
            DestroyProjectile();
        }
    }

    public void PrepareProjectile(Transform startingPoint, float speed, float range)
    {
        transform.position = startingPoint.position;
        transform.rotation = startingPoint.rotation;
        Quaternion rotation = startingPoint.rotation;
        _direction = rotation * Vector3.forward;
        _speed = speed;
        _remainingDistance = range;
    }

    public void FireProjectile()
    {
        _isFired = true;

        foreach (var effect in _effects)
        {
            effect.FireEffect();
        }
    }

    public void MoveProjectile()
    {
        if (_remainingDistance <= 0)
        {
            DestroyProjectile();
        }
        Vector3 previousPosition = transform.position;
        Vector3 newPosition = previousPosition + _direction * (_speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
        _remainingDistance -= Vector3.Distance(previousPosition, newPosition);
    }
    
    public void DestroyProjectile()
    {
        if (_isDestroyed)
        {
            return;
        }
        _isDestroyed = true;
        foreach (var effect in _effects)
        {
            effect.DestroyEffect();
        }

        Destroy(gameObject);
    }

    public void AddEffect(ProjectileEffect effect)
    {
        _effects.Append(effect);
        effect.Initialize(this);
    }
}
