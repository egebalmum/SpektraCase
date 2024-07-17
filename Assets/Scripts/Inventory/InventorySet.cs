using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySet : MonoBehaviour
{
    [SerializeField] private GameItem[] items;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = GetComponent<Inventory>();

        foreach (var item in items)
        {
            _inventory.AddItem(item);
        }
    }
}
