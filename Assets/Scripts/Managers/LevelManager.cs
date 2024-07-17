using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class CharacterLifeCycle
{
    public CharacterCenter character;
    public Transform spawnPoint;
    public int remainingLives;
}
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
            return;
        }
        InitializeCharacters();
    }
    
    private void InitializeCharacters()
    {
        foreach (var characterLifeCycle in characterLifeCycles)
        {
            InitializeCharacter(characterLifeCycle);
        }
    }
    
    private void InitializeCharacter(CharacterLifeCycle characterLifeCycle)
    {
        characterLifeCycle.character = Instantiate(characterLifeCycle.character, characterLifeCycle.spawnPoint.position, Quaternion.identity);
        characterLifeCycle.character.gameObject.name = characterLifeCycle.spawnPoint.gameObject.name;
        characterLifeCycle.character.characterName = characterLifeCycle.spawnPoint.name;

        AdjustCharacterPosition(characterLifeCycle.character);

        if (characterLifeCycle.character.name == mainPlayerName)
        {
            mainPlayerCamera.Follow = characterLifeCycle.character.transform;
        }

        characterLifeCycle.character.OnCharacterDeath += HandleDeath;
        //characterLifeCycle.character.OnCharacterRespawn += HandleRespawn;
    }
    
    private void AdjustCharacterPosition(CharacterCenter character)
    {
        CharacterController characterController = character.GetComponent<CharacterController>();
        NavMeshAgent aiController = character.GetComponent<NavMeshAgent>();
        float offsetY = 0;

        if (characterController != null)
        {
            offsetY = characterController.height / 2;
        }
        else if (aiController != null)
        {
            offsetY = aiController.height / 2;
        }

        character.transform.position += Vector3.up * offsetY;
    }

    public void HandleDeath(CharacterCenter character)
    {
        CharacterLifeCycle lifeCycle = characterLifeCycles.First(cycle => cycle.character == character);
        lifeCycle.remainingLives -= 1;
        
        if (lifeCycle.remainingLives > 0)
        {
            StartCoroutine(HandleRespawn(character, lifeCycle.spawnPoint));
        }
    }

    public IEnumerator HandleRespawn(CharacterCenter character, Transform spawnPoint)
    {
        yield return new WaitForSeconds(respawnTime);
        
        character.transform.position = spawnPoint.position;
        character.transform.rotation = Quaternion.identity;

        AdjustCharacterPosition(character);
        character.Respawn();
    }
}
