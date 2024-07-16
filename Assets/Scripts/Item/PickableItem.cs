using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] private GameItem item;
    private Collider _collider;

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory != null)
        {
            PickItem(inventory);
        }
    }

    private void PickItem(Inventory inventory)
    {
        if (inventory.AddItem(item))
        {
            DestroyPickable();
        }
    }

    private void DestroyPickable()
    {
        Destroy(gameObject);
    }
}
