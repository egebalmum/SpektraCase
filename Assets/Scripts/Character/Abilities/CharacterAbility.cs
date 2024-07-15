using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbility : MonoBehaviour
{
    private bool _isEnabled = true;
    public abstract void Initialize();
    public abstract void EarlyTick();
    public abstract void Tick();
    public abstract void LateTick();

    public virtual void SetAbilityEnabled(bool value)
    {
        _isEnabled = value;
    }

    public virtual  bool GetAbilityEnabled()
    {
        return _isEnabled;
    }

    public void OnEnable()
    {
        SetAbilityEnabled(true);
    }

    public void OnDisable()
    {
        SetAbilityEnabled(false);
    }
}
