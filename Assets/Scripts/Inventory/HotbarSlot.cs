using System;
using UnityEngine;

[Serializable]
public class HotbarSlot
{
    public int SlotIndex;
    public GameItem Item;

    public HotbarSlot(int index)
    {
        SlotIndex = index;
    }

    public void AssignItem(GameItem item)
    {
        Item = item;
    }

    public void ClearSlot()
    {
        Item = null;
    }

    public bool IsEmpty()
    {
        return Item == null;
    }
    
}