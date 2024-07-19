using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FXSpawnEffect : InteractorEffect
{
    [SerializeField] private ParticleSystem fx;
    [SerializeField] private Vector3 upVector;
    [SerializeField] private bool onCold;
    [SerializeField] private bool onHot;
    [SerializeField] private bool onDestroy;


    public override void ColdHitEffect(Collision collision)
    {
        if (!onCold)
        {
            return;
        }
        HandleFX(collision);
    }

    public override void HotHitEffect(Collision collision)
    {
        if (!onHot)
        {
            return;
        }
        HandleFX(collision);
    }

    public override void DestroyEffect()
    {
        if (!onDestroy)
        {
            return;
        }
        HandleFX();
    }

    private void HandleFX(Collision collision)
    {
        var contact = collision.contacts[0];
        Quaternion rotation = Quaternion.LookRotation(contact.normal);
        Instantiate(fx, contact.point, rotation);
    }
    
    private void HandleFX()
    {
        Instantiate(fx, Interactor.transform.position, Quaternion.identity);
    }
}
