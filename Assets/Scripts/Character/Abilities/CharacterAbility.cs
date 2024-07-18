using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbility : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    protected CharacterCenter characterCenter;

    public virtual void Initialize(CharacterCenter characterCenter)
    {
        this.characterCenter = characterCenter;
    }
    public virtual void EarlyTick() { }
    public virtual void Tick() { }
    public virtual void LateTick() { }
    public virtual void ResetAbility() { }
    public virtual void OnDeath() { }
    public virtual void OnRespawn() { }
    public virtual void HandleInput() { }

    public void SetAbilityEnabled(bool value)
    {
        isEnabled = value;
    }

    public  bool GetAbilityEnabled()
    {
        return isEnabled;
    }
}
