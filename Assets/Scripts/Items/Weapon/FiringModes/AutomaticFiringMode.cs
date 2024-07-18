using System;
using System.Collections;
using UnityEngine;

public class AutomaticFiringMode : FiringMode
{

    public override void Fire(float fireRate, Action shoot, Action setTriggerReady)
    {
        StartCoroutine(FireCoroutine(fireRate, shoot, setTriggerReady));
    }
    
    private IEnumerator FireCoroutine(float fireRate,Action shoot, Action setTriggerReady)
    {
        shoot();
        yield return new WaitForSeconds(fireRate);
        setTriggerReady();
    }

    public override bool CheckInput()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
}