using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public string mainPlayerName;
    public CinemachineVirtualCamera mainPlayerCamera;
    public CharacterLifeCycle[] characterLifeCycles;
    public float respawnTime = 3f;
    public static LevelManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        foreach (var characterLifeCycle in characterLifeCycles)
        {
           characterLifeCycle.character = Instantiate(characterLifeCycle.character, characterLifeCycle.spawnPoint.position, Quaternion.identity);
           characterLifeCycle.character.name = characterLifeCycle.spawnPoint.name;
           characterLifeCycle.character.characterName = characterLifeCycle.spawnPoint.name;
           CharacterController characteController = characterLifeCycle.character.GetComponent<CharacterController>();
           NavMeshAgent aiController = characterLifeCycle.character.GetComponent<NavMeshAgent>();
           float offsetY = 0;
           if (characteController != null)
           {
               offsetY = characterLifeCycle.character.GetComponent<CharacterController>().height / 2;
           }
           else if (aiController != null)
           {
               offsetY = characterLifeCycle.character.GetComponent<NavMeshAgent>().height / 2;
           }
           characterLifeCycle.character.transform.position += Vector3.up * offsetY;
           if (characterLifeCycle.character.name == mainPlayerName)
           {
               mainPlayerCamera.Follow = characterLifeCycle.character.transform;
           }
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
        CharacterController characteController = character.GetComponent<CharacterController>();
        NavMeshAgent aiController = character.GetComponent<NavMeshAgent>();
        float offsetY = 0;
        if (characteController != null)
        {
            offsetY = character.GetComponent<CharacterController>().height / 2;
        }
        else if (aiController != null)
        {
            offsetY = character.GetComponent<NavMeshAgent>().height / 2;
        }
        character.transform.position += Vector3.up * offsetY;
        character.Respawn();
    }
}
