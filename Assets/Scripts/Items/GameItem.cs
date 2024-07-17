using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameItem : MonoBehaviour
{
    public string name;
    public bool isActive;
    [HideInInspector] public CharacterCenter owner;
    public virtual void SetItemActive(bool value)
    {
        isActive = value;
    }
    public virtual void HoldItem() { }
    public virtual void ReleaseItem() { }

    public virtual void Initialize(CharacterCenter _owner)
    {
        owner = _owner;
    }
    
}
