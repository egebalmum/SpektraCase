using System;
using UnityEngine;

[Serializable]
public class HotbarSlot
{
    public int SlotIndex;
    public GameItem Item;
    public Action<Sprite> OnHotbarSlotChanged;

    public HotbarSlot(int index)
    {
        SlotIndex = index;
    }

    public void AssignItem(GameItem item)
    {
        Item = item;
        OnHotbarSlotChanged?.Invoke(item.image);
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