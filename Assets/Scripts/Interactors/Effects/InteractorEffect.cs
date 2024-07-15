using System;
using UnityEngine;

[Serializable]
public abstract class InteractorEffect : MonoBehaviour
{
    protected Interactor Interactor;
    public virtual void Initialize(Interactor interactor)
    {
        Interactor = interactor;
    }
    public virtual void FireEffect() { }

    public virtual void HotHitEffect(Collision collision) { }
    public virtual void HotHitEffect(Collider other) { }

    public virtual void ColdHitEffect(Collision collision) { }
    public virtual void ColdHitEffect(Collider other) { }
    public virtual void UpdateEffect() { }
    public virtual void DestroyEffect() { }
    public virtual void ResetEffect() { }
}