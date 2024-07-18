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
    [HideInInspector] public List<FiringMode> firingModes;
    public String mode;
    [SerializeField] private GameObject visuals;
    
    private FiringMode _currentFiringMode;
    private bool _isTriggerReady = true;
    private bool _isPlayerControlled;

    public override void Initialize(CharacterCenter _owner)
    {
        base.Initialize(_owner);
        projectile = Instantiate(projectile, Vector3.zero, Quaternion.identity);
        projectile.gameObject.SetActive(false);
        InstantiateChildInteractors(projectile);
        weaponUpgrades = new List<WeaponUpgrade>();
        _isPlayerControlled = owner.characterName.Equals(LevelManager.instance.mainPlayerName);
        firingModes = GetComponents<FiringMode>().ToList();
        SetFiringMode(firingModes[0]);
        owner.OnCharacterDeath += RemoveUpgrades;
    }
    
    private void InstantiateChildInteractors(Interactor interactor)
    {
        InteractorSpawnEffect[] spawnEffects = interactor.GetComponents<InteractorSpawnEffect>();
        if (spawnEffects != null)
        {
            foreach (var spawnEffect in spawnEffects)
            {
                InstantiateChildInteractors(spawnEffect.interactor);
                spawnEffect.interactor = Instantiate(spawnEffect.interactor, Vector3.zero, Quaternion.identity);
                spawnEffect.interactor.gameObject.SetActive(false);
            }
        }
    }

    public override void Tick()
    {
        if (!isActive)
        {
            return;
        }

        if (!_isPlayerControlled)
        {
            return;
        }
        if (_isTriggerReady && Input.GetKeyDown(KeyCode.C))
        {
            ChangeFiringMode();
        }
        
        if (_currentFiringMode != null && _currentFiringMode.CheckInput())
        {
            TryShoot();
        }
    }

    public void TryShoot()
    {
        if (!isActive)
        {
            return;
        }
        if (_isTriggerReady)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        _currentFiringMode.Fire(fireRate, TriggerWeapon, SetTriggerReady);
        SetTriggerReady(false);
    }

    public void ChangeFiringMode()
    {
        int index = firingModes.IndexOf(_currentFiringMode);
        int newIndex = (index + 1) % firingModes.Count;
        SetFiringMode(firingModes[newIndex]);
    }
    public void SetFiringMode(FiringMode newFiringMode)
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
        visuals.SetActive(value);
    }

    public bool AllowedToAdd(WeaponUpgrade upgrade)
    {
        return !weaponUpgrades.Any(element => element.GetType() == upgrade.GetType());
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