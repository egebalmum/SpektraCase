using System.Collections;
using System.Collections.Generic;
using MPUIKIT;
using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private MPImageBasic armorImage;
    public string characterName;
    public bool isWorldSpace;
    
    public void SetBarValue(float value)
    {
        armorImage.fillAmount = value;
    }
}
