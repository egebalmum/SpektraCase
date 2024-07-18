using System;
using Unity.VisualScripting;
using UnityEngine;
public class Health : CharacterAbility
{
    [SerializeField] private float startHealthPoint = 100;
    private float _currentHealth;
    private CharacterCenter _character;

    public Action<float> OnHealthChange;

    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        _character = GetComponent<CharacterCenter>();
        _currentHealth = startHealthPoint;
    }

    public void ApplyDamageInstant(float value)
    {
        if (!GetAbilityEnabled())
        {
            return;
        }
        _currentHealth -= value;
        OnHealthChange?.Invoke(_currentHealth/startHealthPoint);
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Death();
        }
    }

    public float GetHealthPoint()
    {
        return _currentHealth;
    }

    private void Death()
    {
        _character.effectState = CharacterEffectState.Death;
        _character.Death();
    }

    public override void ResetAbility()
    {
        _currentHealth = startHealthPoint;
        OnHealthChange?.Invoke(_currentHealth/startHealthPoint);
    }

    public override void OnRespawn()
    {
        ResetAbility();
        SetAbilityEnabled(true);
    }

    public override void OnDeath()
    {
        SetAbilityEnabled(false);
    }
}
