using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeaponUpgrade : MonoBehaviour
{
    public WeaponUpgrade upgrade;
    private Collider _collider;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractPickable>() == null)
        {
            return;
        }
        PickUpgrade(other);
    }

    private void PickUpgrade(Collider other)
    {
        CharacterHandleHand handleHand = other.GetComponent<CharacterHandleHand>();
        if (handleHand == null)
        {
            return;
        }

        if (handleHand.GetHandItem() == null)
        {
            return;
        }
        if (handleHand.GetHandItem().GetType() != typeof(Weapon))
        {
            return;
        }
        Weapon weapon = (Weapon)handleHand.GetHandItem();
        if (!weapon.AllowedToAdd(upgrade))
        {
            return;
        }
        weapon.AddUpgrade(upgrade);
        DestroyPickable();
    }
    
    private void DestroyPickable()
    {
        gameObject.SetActive(false);
    }
    

}
