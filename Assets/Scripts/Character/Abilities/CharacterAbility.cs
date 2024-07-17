using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbility : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;

    public virtual void Initialize() { }
    public virtual void EarlyTick() { }
    public virtual void Tick() { }
    public virtual void LateTick() { }
    public virtual void ResetAbility() { }
    public virtual void OnDeath() { }
    public virtual void OnRespawn() { }
    public virtual void HandleInput() { }

    public virtual void SetAbilityEnabled(bool value)
    {
        isEnabled = value;
    }

    public virtual  bool GetAbilityEnabled()
    {
        return isEnabled;
    }
}
