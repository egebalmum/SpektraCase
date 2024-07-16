using System;
using System.Collections;
using UnityEngine;

public class AutomaticFiringMode : MonoBehaviour ,IFiringMode
{
    public string GetModeName()
    {
        return "Auto";
    }

    public void Fire(float fireRate, Action shoot, Action setTriggerReady)
    {
        StartCoroutine(FireCoroutine(fireRate, shoot, setTriggerReady));
    }
    
    private IEnumerator FireCoroutine(float fireRate,Action shoot, Action setTriggerReady)
    {
        shoot();
        yield return new WaitForSeconds(fireRate);
        setTriggerReady();
    }

    public bool CheckInput()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }
}