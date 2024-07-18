using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCenter : MonoBehaviour
{
    public CharacterMovementState movementState = CharacterMovementState.Idle;
    public CharacterEffectState effectState = CharacterEffectState.Idle;
    [HideInInspector] public bool isPlayerControlled = false;
    [SerializeField] private GameObject[] characterVisuals;
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
            ability.Initialize(this);
        }
    }

    private void ProcessAbilitiesTicks()
    {
        foreach (var ability in _abilities)
        {
            if (ability.GetAbilityEnabled() && !ability.blockedMoveStates.Contains(movementState) && !ability.blockedEffectStates.Contains(effectState))
            {
                ability.Tick();
            }
        }
    }

    private void ProcessAbilitiesLateTicks()
    {
        foreach (var ability in _abilities)
        {
            if (ability.GetAbilityEnabled() && !ability.blockedMoveStates.Contains(movementState) && !ability.blockedEffectStates.Contains(effectState))
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
        SetVisuals(false);
    }

    private void SetVisuals(bool value)
    {
        foreach (var visualObject in characterVisuals)
        {
            visualObject.SetActive(value);
        }
    }

    public void Respawn()
    {
        movementState = CharacterMovementState.Idle;
        effectState = CharacterEffectState.Idle;
        SetVisuals(true);
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

    public void SetMovementState(CharacterMovementState newState)
    {
        movementState = newState;
    }

    public void SetEffectState(CharacterEffectState newState)
    {
        effectState = newState;
    }
}
