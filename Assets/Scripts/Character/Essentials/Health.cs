using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float healthPoint = 100;
    public void InstantDamage(float value)
    {
        healthPoint -= value;
    }

    public float GetHealthPoint()
    {
        return healthPoint;
    }
    
}
