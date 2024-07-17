using System;
using System.Collections;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Hotbar hotbar;
    private CharacterCenter _owner;

    public void Start()
    {
        _owner = GetComponent<CharacterCenter>();
        hotbar.Initialize();
    }

    public void Update()
    {
        hotbar.OnUpdate();
    }

    public bool AddItem(GameItem item)
    {
        if (hotbar.GetEmptyIndex() == -1)
        {
            return false;
        }

        GameItem _item = Instantiate(item, _owner.transform);
        _item.transform.localPosition = Vector3.zero;
        _item.Initialize(_owner);
        _item.SetItemActive(false);
        hotbar.AssignItemToSlot(hotbar.GetEmptyIndex(), _item);
        return true;
    }
}
