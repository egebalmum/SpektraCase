using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private float armorPoint = 100;
    public void InstantDamage(float value)
    {
       armorPoint -= value;
    }

    public float getArmorPoint()
    {
        return armorPoint;
    }
    
}
