using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : GameItem
{
    public float range;
    public float speed;
    public float fireRate;
    public Transform firePoint;
    private IFiringMode currentFiringMode;
    public Projectile projectile;
    private bool _isTriggerReady = true;
    public List<IFiringMode> firingModes;
    public String mode;

    void Start()
    {
        firingModes = GetComponents<IFiringMode>().ToList();
        SetFiringMode(firingModes[0]);
    }
    
    void Update()
    {
        if (_isTriggerReady && Input.GetKeyDown(KeyCode.C))
        {
            ChangeFiringMode();
        }
        
        if (currentFiringMode != null && currentFiringMode.CheckInput() && _isTriggerReady)
        {
            currentFiringMode.Fire(fireRate, TriggerWeapon, SetTriggerReady);
            SetTriggerReady(false);
        }
    }

    public void ChangeFiringMode()
    {
        int index = firingModes.IndexOf(currentFiringMode);
        int size = firingModes.Count;
        int newIndex = (index + 1) % size;
        SetFiringMode(firingModes[newIndex]);
    }
    public void SetFiringMode(IFiringMode newFiringMode)
    {
        currentFiringMode = newFiringMode;
        mode = currentFiringMode.GetModeName();
    }

    private void TriggerWeapon()
    {
        Projectile _projectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        _projectile.InitializeInteractor(owner);
        _projectile.ResetProjectile();
        _projectile.PrepareProjectile(firePoint, speed, range);
        _projectile.FireProjectile();
    }

    private void SetTriggerReady()
    {
        _isTriggerReady = true;
    }

    private void SetTriggerReady(bool value)
    {
        _isTriggerReady = value;
    }
}