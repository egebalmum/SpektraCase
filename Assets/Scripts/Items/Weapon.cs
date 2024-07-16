using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : GameItem
{
    public List<WeaponUpgrade> weaponUpgrades;
    public float range;
    public float speed;
    public float fireRate;
    public Transform firePoint;
    public Projectile projectile;
    public List<IFiringMode> firingModes;
    public String mode;
    [SerializeField] private GameObject visuals;
    private IFiringMode _currentFiringMode;
    private bool _isTriggerReady = true;

    public override void Initialize(CharacterCenter _owner)
    {
        projectile = Instantiate(projectile, Vector3.zero, Quaternion.identity);
        projectile.gameObject.SetActive(false);
        weaponUpgrades = new List<WeaponUpgrade>();
        owner = _owner;
        firingModes = GetComponents<IFiringMode>().ToList();
        SetFiringMode(firingModes[0]);
        owner.OnCharacterDeath += RemoveUpgrades;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (_isTriggerReady && Input.GetKeyDown(KeyCode.C))
        {
            ChangeFiringMode();
        }
        
        if (_currentFiringMode != null && _currentFiringMode.CheckInput() && _isTriggerReady)
        {
            _currentFiringMode.Fire(fireRate, TriggerWeapon, SetTriggerReady);
            SetTriggerReady(false);
        }
    }

    public void ChangeFiringMode()
    {
        int index = firingModes.IndexOf(_currentFiringMode);
        int size = firingModes.Count;
        int newIndex = (index + 1) % size;
        SetFiringMode(firingModes[newIndex]);
    }
    public void SetFiringMode(IFiringMode newFiringMode)
    {
        _currentFiringMode = newFiringMode;
        mode = _currentFiringMode.GetModeName();
    }

    private void TriggerWeapon()
    {
        Projectile _projectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        _projectile.gameObject.SetActive(true);
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

    public override void SetItemActive(bool value)
    {
        base.SetItemActive(value);
        if (value)
        {
            visuals.SetActive(true);
        }
        else
        {
            visuals.SetActive(false);
        }
    }

    public bool AllowedToAdd(WeaponUpgrade upgrade)
    {
        WeaponUpgrade upgradeFromList = weaponUpgrades.FirstOrDefault(element => element.GetType() == upgrade.GetType());
        if (upgradeFromList != null)
        {
            return false;
        }
        return true;
    }

    public void AddUpgrade(WeaponUpgrade upgrade)
    {
        weaponUpgrades.Add(upgrade);
        upgrade.AddUpgrade(this);
    }

    private void RemoveUpgrades(CharacterCenter character)
    {
        foreach (var upgrade in weaponUpgrades)
        {
            upgrade.RemoveUpgrade(this);
        }
        weaponUpgrades.Clear();
    }
    
}