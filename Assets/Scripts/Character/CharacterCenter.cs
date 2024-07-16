using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCenter : MonoBehaviour
{
    private List<CharacterAbility> _abilities;
    

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

    public void OnDeath()
    {
           
    }

    public void OnRespawn()
    {
        
    }
}