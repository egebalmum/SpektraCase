using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private Vector3 offset; 
    
    void LateUpdate()
    {
        CharacterCenter character =  FindObjectsOfType<CharacterCenter>().First(character => character.name == characterName);
        
        if (character != null)
        {
            transform.position = character.transform.position + offset;
        }
    }
}
