using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : InteractorEffect
{
    [SerializeField] private float time;
    
    public override void HotHitEffect(Collision collision)
    {
        HotHitEffect(collision.collider);
    }

    public override void HotHitEffect(Collider other)
    {
       CharacterStunability characterStunability = other.GetComponent<CharacterStunability>();
       if (characterStunability != null)
       {
           characterStunability.StartStun(time);
       }
    }

}
