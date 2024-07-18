using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlotView : MonoBehaviour
{
    [SerializeField] private Image slotImage;

    public void ChangeSlotImage(Sprite sprite)
    {
        slotImage.sprite = sprite;
    }
}
