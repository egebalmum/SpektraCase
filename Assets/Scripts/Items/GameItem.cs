using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameItem : MonoBehaviour
{
    [HideInInspector] public CharacterCenter owner;
    public string name;
    public bool isActive;

    public virtual void SetItemActive(bool value)
    {
        isActive = value;
    }
    public virtual void HoldItem()
    {
        
    }
    public virtual void ReleaseItem() {}
    public virtual void Initialize(CharacterCenter owner) { }
    
}
