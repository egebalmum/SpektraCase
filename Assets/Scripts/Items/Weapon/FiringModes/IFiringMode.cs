using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public interface IFiringMode
{
    String GetModeName();
    void Fire(float fireRate, Action shoot, Action setTriggerReady);
    bool CheckInput();
}


