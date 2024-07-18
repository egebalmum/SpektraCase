using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class FiringMode : MonoBehaviour
{
    public String GetModeName()
    {
        return GetType().ToString();
    }
    public abstract void Fire(float fireRate, Action shoot, Action setTriggerReady);
    public abstract bool CheckInput();
}


