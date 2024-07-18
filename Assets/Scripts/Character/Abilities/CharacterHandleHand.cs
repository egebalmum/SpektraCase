using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandleHand : CharacterAbility
{
    [SerializeField] private Transform hand;
    private Inventory _inventory;
    private GameItem _onHand;

    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        _inventory = GetComponent<Inventory>();
        _inventory.hotbar.OnSelectedHotBarEffected += HandItemSwitch;
    }

    private void HandItemSwitch()
    {
        if (!GetAbilityEnabled())
        {
            return;
        }

        GameItem selectedItem = _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item;

        if (selectedItem == null)
        {
            ClearHand();
        }
        else
        {
            EquipItemInHand(selectedItem);
        }
    }

    public override void Tick()
    {
        if (_onHand != null && _onHand.isActive)
        {
            _onHand.Tick();
        }
    }

    private void ClearHand()
    {
        if (_onHand != null)
        {
            _onHand.SetItemActive(false);
            _onHand = null;
            characterCenter.animator.SetTrigger("HandDefault");
        }
    }

    private void EquipItemInHand(GameItem item)
    {
        if (_onHand != null)
        {
            _onHand.SetItemActive(false);
        }
        item.transform.parent = hand;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        item.SetItemActive(true);
        _onHand = item;
        characterCenter.animator.SetTrigger("Hand" + item.type);
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
            characterCenter.animator.SetTrigger("Hand" + _onHand.type);
        }
    }

    public GameItem GetHandItem()
    {
        return _onHand;
    }
}