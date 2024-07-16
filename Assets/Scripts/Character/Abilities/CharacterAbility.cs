using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbility : MonoBehaviour
{
    private bool _isEnabled = true;

    public virtual void Initialize() { }
    public virtual void EarlyTick() { }
    public virtual void Tick() { }
    public virtual void LateTick() { }
    public virtual void ResetAbility() { }
    public virtual void OnDeath() { }
    public virtual void OnRespawn() { }

    public virtual void SetAbilityEnabled(bool value)
    {
        _isEnabled = value;
    }

    public virtual  bool GetAbilityEnabled()
    {
        return _isEnabled;
    }
}
