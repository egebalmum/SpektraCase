using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MPUIKIT;
using UnityEngine;

public class ArmorBarView : MonoBehaviour
{
    [SerializeField] private MPImageBasic armorImage;
    public string characterName;
    public bool isWorldSpace;

    public void SetBarValue(float value)
    {
        armorImage.fillAmount = value;
    }
}
