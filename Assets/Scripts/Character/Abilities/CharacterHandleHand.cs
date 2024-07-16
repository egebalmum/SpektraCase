using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandleHand : CharacterAbility
{
    [SerializeField] private Transform hand;
    private Inventory _inventory;
    private GameItem _onHand;

    public override void Initialize()
    {
        _inventory = GetComponent<Inventory>();
        _inventory.hotbar.OnHotbarEffected += HandItemSwitch;

    }

    public override void EarlyTick()
    {
        
    }

    public override void Tick()
    {
        
    }

    public override void LateTick()
    {
        
    }

    private void HandItemSwitch()
    {
        if (!GetAbilityEnabled())
        {
            return;
        }
        
        if (_inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item == null)
        {
            if (_onHand != null)
            {
                _onHand.SetItemActive(false);
                _onHand = null;
            }
        }
        else
        {
            if (_onHand != null)
            {
                _onHand.SetItemActive(false);
            }

            _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item.transform.parent = hand;
            _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item.transform.localPosition = Vector3.zero;
            _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item.transform.localRotation = Quaternion.identity;
            _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item.SetItemActive(true);
            _onHand = _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item;
        }
    }

    public override void OnDeath()
    {
        if (_onHand != null)
        {
            _onHand.SetItemActive(false);
        }
    }

    public override void OnRespawn()
    {
        if (_onHand != null)
        {
            _onHand.SetItemActive(true);
        }
    }

    public GameItem GetHandItem()
    {
        return _onHand;
    }
}
