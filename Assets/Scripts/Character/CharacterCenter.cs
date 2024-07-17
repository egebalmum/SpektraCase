using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCenter : MonoBehaviour
{
    [SerializeField] private GameObject characterVisuals;
    [HideInInspector] public string characterName;
    
    private List<CharacterAbility> _abilities;
    public Action<CharacterCenter> OnCharacterDeath;

    void Start()
    {
        FindAbilities();
        InitializeAbilities();
    }

    void Update()
    {
        ProcessAbilitiesEarlyTicks();
        ProcessAbilitiesTicks();
        ProcessAbilitiesLateTicks();
    }

    private void FindAbilities()
    {
        _abilities = GetComponents<CharacterAbility>().ToList();
    }

    private void InitializeAbilities()
    {
        foreach (var ability in _abilities)
        {
            ability.Initialize();
        }
    }

    private void ProcessAbilitiesEarlyTicks()
    {
        foreach (var ability in _abilities)
        {
            if (ability.GetAbilityEnabled())
            {
                ability.EarlyTick();
            }
        }
    }

    private void ProcessAbilitiesTicks()
    {
        foreach (var ability in _abilities)
        {
            if (ability.GetAbilityEnabled())
            {
                ability.Tick();
            }
        }
    }

    private void ProcessAbilitiesLateTicks()
    {
        foreach (var ability in _abilities)
        {
            if (ability.GetAbilityEnabled())
            {
                ability.LateTick();
            }
        }
    }

    public void Death()
    {
        OnCharacterDeath?.Invoke(this);
        foreach (var ability in _abilities)
        {
            ability.SetAbilityEnabled(false);
            ability.OnDeath();
        }
        SetCollidersEnabled(false);
        characterVisuals.SetActive(false);
    }

    public void Respawn()
    {
        characterVisuals.SetActive(true);
        SetCollidersEnabled(true);
        foreach (var ability in _abilities)
        {
            ability.SetAbilityEnabled(true);
            ability.OnRespawn();
        }
    }

    private void SetCollidersEnabled(bool enabled)
    {
        var colliders = GetComponents<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = enabled;
        }
    }
}
