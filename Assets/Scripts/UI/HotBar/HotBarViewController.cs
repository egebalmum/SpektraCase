using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotBarViewController : MonoBehaviour
{
    [SerializeField] private Transform hotBarSlotViewParent;
    [SerializeField] private HotBarSlotView slotView;


    private void Start()
    {
        CharacterCenter center = FindObjectsOfType<CharacterCenter>()
            .First(center => center.characterName.Equals(LevelManager.instance.mainPlayerName));
        Hotbar bar = center.GetComponent<Inventory>().hotbar;
        for (int i = 0; i < bar.hotbarSize; i++)
        {
            HotBarSlotView slotViewInstance = Instantiate(slotView, hotBarSlotViewParent);
            slotViewInstance.transform.localPosition = Vector3.zero;
            bar.hotbarSlots[i].OnHotbarSlotChanged += slotViewInstance.ChangeSlotImage;
        }
    }
}
