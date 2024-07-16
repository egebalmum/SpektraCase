using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Hotbar
{
    public HotbarSlot[] hotbarSlots;
    public int selectedIndex = 0;
    [SerializeField] private int hotbarSize = 5;
    public event Action OnHotbarEffected;

    public void Initialize()
    {
        InitializeSlots(hotbarSize); 
    }

    public void OnUpdate()
    {
        HandleInput();
    }

    private void InitializeSlots(int numberOfSlots)
    {
        hotbarSlots = new HotbarSlot[numberOfSlots];
        for (int i = 0; i < numberOfSlots; i++)
        {
            hotbarSlots[i] = new HotbarSlot(i);
        }
    }

    private void HandleInput()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)('1' + i)))
            {
                SelectSlot(i);
            }
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length || index == selectedIndex)
        {
            return;
        }
        selectedIndex = index;
        OnHotbarEffected?.Invoke();
    }
    

    public void AssignItemToSlot(int slotIndex, GameItem item)
    {
        if (slotIndex < 0 || slotIndex >= hotbarSlots.Length)
        {
            return;
        }
        hotbarSlots[slotIndex].AssignItem(item);
        if (selectedIndex == slotIndex)
        {
            OnHotbarEffected?.Invoke();
        }
        
    }

    public int GetEmptyIndex()
    {
        foreach (var slot in hotbarSlots)
        {
            if (slot.IsEmpty())
            {
                return slot.SlotIndex;
            }
        }

        return -1;
    }
}