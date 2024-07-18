using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySet : CharacterAbility
{
    [SerializeField] private GameItem[] items;
    private Inventory _inventory;

    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        
    }

    private void Start()
    {
        SetItems();
    }

    private void SetItems()
    {
        _inventory = GetComponent<Inventory>();

        foreach (var item in items)
        {
            _inventory.AddItem(item);
        }
    }
}
