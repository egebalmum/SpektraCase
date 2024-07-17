using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CharacterLifeCycle[] characterLifeCycles;
    public float respawnTime = 3f;
    
    public void Awake()
    {
        foreach (var characterLifeCycle in characterLifeCycles)
        {
           characterLifeCycle.character = Instantiate(characterLifeCycle.character, characterLifeCycle.spawnPoint.position, Quaternion.identity);
           float offsetY = characterLifeCycle.character.GetComponent<CharacterController>().height / 2;
           characterLifeCycle.character.transform.position += Vector3.up * offsetY;
           characterLifeCycle.character.name = characterLifeCycle.spawnPoint.name;
           characterLifeCycle.character.OnCharacterDeath += HandleDeath;
        }
    }

    public void HandleDeath(CharacterCenter character)
    {
        CharacterLifeCycle lifeCycle = characterLifeCycles.First(cycle => cycle.character == character);
        lifeCycle.remainingLives -= 1;
        if (lifeCycle.remainingLives <= 0)
        {
            return;
        }

        StartCoroutine(HandleRespawn(character, lifeCycle.spawnPoint));

    }

    public IEnumerator HandleRespawn(CharacterCenter character, Transform transform)
    {
        yield return new WaitForSeconds(respawnTime);
        
        character.transform.position = transform.position;
        character.transform.rotation = Quaternion.identity;
        float offsetY = character.GetComponent<CharacterController>().height / 2;
        character.transform.position += Vector3.up * offsetY;
        character.Respawn();
    }
}
