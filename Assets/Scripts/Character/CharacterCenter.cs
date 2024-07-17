using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCenter : MonoBehaviour
{
    [HideInInspector] public string characterName;
    private List<CharacterAbility> _abilities;
    public Action<CharacterCenter> OnCharacterDeath;
    [SerializeField] private GameObject characterVisuals;

    void Start()
    {
        FindAbilities();
        AbilityInitializes();
    }
    
    void Update()
    {
        AbilityEarlyTicks();
        AbilityTicks();
        AbilityLateTicks();
    }

    private void FindAbilities()
    {
        _abilities = GetComponents<CharacterAbility>().ToList();
    }
    private void AbilityInitializes()
    {
        foreach (CharacterAbility ability in _abilities)
        {
            ability.Initialize();
        }
    }

    private void AbilityEarlyTicks()
    {
        foreach (CharacterAbility ability in _abilities)
        {
            if (!ability.GetAbilityEnabled())
            {
                continue;
            }
            ability.EarlyTick();
        }
    }
    
    private void AbilityTicks()
    {
        foreach (CharacterAbility ability in _abilities)
        {
            if (!ability.GetAbilityEnabled())
            {
                continue;
            }
            ability.Tick();
        }
    }
    
    private void AbilityLateTicks()
    {
        foreach (CharacterAbility ability in _abilities)
        {
            if (!ability.GetAbilityEnabled())
            {
                continue;
            }
            ability.LateTick();
        }
    }
    public void Death()
    {
        OnCharacterDeath?.Invoke(this);
        foreach (CharacterAbility ability in _abilities)
        {
            ability.SetAbilityEnabled(false);
            ability.OnDeath();
        }
        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        characterVisuals.SetActive(false);
    }

    public void Respawn()
    {
        characterVisuals.SetActive(true);
        Collider[] colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
        foreach (CharacterAbility ability in _abilities)
        {
            ability.SetAbilityEnabled(true);
            ability.OnRespawn();
        }
    }
}
