using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class Projectile : Interactor
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
    private List<InteractorEffect> _effects;
    
    public override void InitializeInteractor(CharacterCenter _owner)
    {
        owner = _owner;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        _effects = GetComponents<InteractorEffect>().ToList();
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
        if (_remainingDistance <= 0)
        {
            DestroyInteractor();
        }
        MoveProjectile();
    }

    private void Update()
    {
        if (!_isFired)
        {
            return;
        }
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
            DestroyInteractor();
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

    public override void PrepareInteractor(Transform startingPoint)
    {
        transform.position = startingPoint.position;
        transform.rotation = startingPoint.rotation;
        Quaternion rotation = startingPoint.rotation;
        _direction = rotation * Vector3.forward;
        _speed = 40;
        _remainingDistance = 100;
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
        _rigidbody.velocity = _direction * _speed;
        Vector3 previousPosition = transform.position;
        Vector3 newPosition = previousPosition + _direction * (_speed * Time.fixedDeltaTime);
        //_rigidbody.MovePosition(newPosition);
        _remainingDistance -= Vector3.Distance(previousPosition, newPosition);
    }

    public override void DestroyInteractor()
    {
        base.DestroyInteractor();
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
}
