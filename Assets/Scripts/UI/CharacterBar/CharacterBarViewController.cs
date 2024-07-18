using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterBarViewController : MonoBehaviour
{
    private HealthBarView[] _healthBarViews;
    private ArmorBarView[] _armorBarViews;
    void Start()
    {
        _healthBarViews = FindObjectsOfType<HealthBarView>();
        _armorBarViews = FindObjectsOfType<ArmorBarView>();
        CharacterCenter[] characters = FindObjectsOfType<CharacterCenter>();
        foreach (var healthBar in _healthBarViews)
        {
            if (healthBar.isWorldSpace)
            {
                healthBar.characterName = healthBar.GetComponentInParent<CharacterCenter>().characterName;
            }
            CharacterCenter character = characters.First(character => character.characterName.Equals(healthBar.characterName));
            Health health = character.GetComponent<Health>();
            health.OnHealthChange += healthBar.SetBarValue;
        }

        foreach (var armorBar in _armorBarViews)
        {
            if (armorBar.isWorldSpace)
            {
                armorBar.characterName = armorBar.GetComponentInParent<CharacterCenter>().characterName;
            }
            CharacterCenter character = characters.First(character => character.characterName.Equals(armorBar.characterName));
            Armor armor = character.GetComponent<Armor>();
            armor.OnArmorChange += armorBar.SetBarValue;
        }
    }

}
