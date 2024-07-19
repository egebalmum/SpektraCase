using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Area : Interactor
{
    [SerializeField] private float areaRadius = 7f;
    [SerializeField] private LayerMask areaLayerMask;
    [SerializeField] private float areaDelay = 10f;
    [SerializeField] private GameObject models;
    [SerializeField] private bool isLineOfSightNeeded;
    private Collider _collider;
    private List<InteractorEffect> _effects;
    private bool _isCasted;
    private bool _isExecuted;
    private float _effectDelay;
    private List<Collider> _insideColliders = new List<Collider>();
    
    public override void InitializeInteractor(CharacterCenter _owner)
    {
        owner = _owner;
        _collider = GetComponent<Collider>();
        if (_collider is SphereCollider sphereCollider)
        {
            sphereCollider.radius = areaRadius;
        }
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

    private void Update()
    {
        if (!_isCasted || _isExecuted)
        {
            return;
        }
        if (_effectDelay <= 0)
        {
            _isExecuted = true;
            //DestroyInteractor();
            DestroyImmediate();
            return;
        }
        
        _effectDelay -= Time.deltaTime;
        foreach (var effect in _effects)
        {
            effect.UpdateEffect();
        }
    }
    
    public override void ResetInteractor()
    {
        _isCasted = false;
        _isExecuted = false;
        _effectDelay = areaDelay;
        _insideColliders.Clear();

        if (_effects == null)
        {
            return;
        }
        
        foreach (var effect in _effects)
        {
            effect.ResetEffect();
        }
    }
    
    public override void PrepareInteractor(Transform startingPoint)
    {
        transform.position = startingPoint.position;
        transform.rotation = startingPoint.rotation;
    }
    
    public override void CastInteractor()
    {
        ExplosionDebugManager.RegisterExplosion(transform.position, areaRadius, Color.yellow);
        _isCasted = true;
        
        foreach (var effect in _effects)
        {
            effect.FireEffect();
        }

        
    }
    
    private void DestroyImmediate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, areaRadius, areaLayerMask)
            .Where(collider => (collider.excludeLayers.value & (1 << gameObject.layer)) == 0)
            .ToArray();
        _insideColliders.AddRange(hitColliders);
        DestroyInteractor();
        _insideColliders.Clear();
    }

    public override void DestroyInteractor()
    {
        ExplosionDebugManager.RegisterExplosion(transform.position, areaRadius, Color.red);
        if (isLineOfSightNeeded)
        {
            _insideColliders.RemoveAll(collider => !HasLineOfSight(collider.transform));
        }
        foreach (var effect in _effects)
        {
            foreach (var collider in _insideColliders)
            {
                if (collider.GetComponent<Health>() != null)
                {
                    effect.HotHitEffect(collider);
                }
            }
            effect.DestroyEffect();
        }
        
        Destroy(gameObject);
    }
    
    public bool HasLineOfSight(Transform otherTransform)
    {
        float distanceToOther = Vector3.Distance(transform.position, otherTransform.position);
        if (distanceToOther <= 0.1f) 
        {
            return true;
        }
        RaycastHit hit;
        Vector3 directionToPlayer = (otherTransform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, areaRadius, areaLayerMask))
        {
            if (hit.transform == otherTransform)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (((1 << other.gameObject.layer) & areaLayerMask) != 0)
        //{
        //    _insideColliders.Add(other);
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (_insideColliders.Contains(other))
        //{
        //    _insideColliders.Remove(other);
        //}
    }
}
