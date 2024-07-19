using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStunability : CharacterAbility
{
    private bool _isStunned;
    private float _remainingTime;


    public override void Tick()
    {
        if (!_isStunned)
        {
            return;
        }
        if (_remainingTime <= 0)
        {
            _isStunned = false;
            characterCenter.SetEffectState(CharacterEffectState.Idle);
            characterCenter.animator.speed = 1;
        }

        _remainingTime -= Time.deltaTime;
    }

    public void StartStun(float time)
    {
        if (!GetAbilityEnabled())
        {
            return;
        }
        if (_isStunned)
        {
            return;
        }

        _isStunned = true;
        _remainingTime = time;
        characterCenter.SetEffectState(CharacterEffectState.Stunned);
        characterCenter.animator.speed = 0;
    }

    public override void OnDeath()
    {
        _isStunned = false;
        _remainingTime = 0;
    }
}
