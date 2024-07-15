using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform itemParent;
    [SerializeField] private int inventorySize;
    [SerializeField] private GameItem[] inventoryItems;

    private CharacterCenter _character;
    private int _currentSelection = 0;
    private GameItem _currentItem;
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_currentSelection == 1)
            {
                return;
            }

            if (_currentItem != null)
            {
                Destroy(_currentItem.gameObject);
            }
            _currentItem = Instantiate(inventoryItems[0], itemParent);
            _currentItem.transform.localPosition = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_currentSelection == 2)
            {
                return;
            }
            if (_currentItem != null)
            {
                Destroy(_currentItem.gameObject);
            }
            _currentItem = Instantiate(inventoryItems[1], itemParent);
            _currentItem.transform.localPosition = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_currentSelection == 3)
            {
                return;
            }
            if (_currentItem != null)
            {
                Destroy(_currentItem.gameObject);
            }
            _currentItem = Instantiate(inventoryItems[2], itemParent);
            _currentItem.transform.localPosition = Vector3.zero;
        }
    }

    private void Initialize()
    {
        inventoryItems = new GameItem[inventorySize];
        _character = GetComponent<CharacterCenter>();
    }

    public bool AddItem(GameItem item)
    {
        int emptyIndex = GetFirstEmptyIndex();
        if (emptyIndex == -1)
        {
            Debug.LogError("Inventory is full");
            return false;
        }
        inventoryItems[emptyIndex] = item;
        item.owner = _character;
        return true;
    }

    private int GetFirstEmptyIndex()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameItem item = inventoryItems[i];
            if (item == null)
            {
                return i;
            }
        }

        return -1;
    }
    
}
