using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandleHand : MonoBehaviour
{
    private Inventory _inventory;
    private GameItem _onHand;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();
    }

    public void Update()
    {
        if (_inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item == null)
        {
            if (_onHand != null)
            {
                _onHand.SetActive(false);
                _onHand = null;
            }
        }
        else
        {
            if (_onHand != null)
            {
                _onHand.SetActive(false);
            }
            _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item.SetActive(true);
            _onHand = _inventory.hotbar.hotbarSlots[_inventory.hotbar.selectedIndex].Item;
        }
    }
    
}
