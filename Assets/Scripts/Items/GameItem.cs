using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public enum GameItemType
{
    Default,
    WeaponSingle,
    WeaponDouble,
    WeaponShoulder
}
public class GameItem : MonoBehaviour
{
    public Vector3 onHandPosition;
    public GameItemType type;
    public Sprite image;
    public string itemName;
    public bool isActive;
    [HideInInspector] public CharacterCenter owner;
    public virtual void SetItemActive(bool value)
    {
        isActive = value;
    }

    public virtual void Tick() { }
    public virtual void HoldItem() { }
    public virtual void ReleaseItem() { }

    public virtual void Initialize(CharacterCenter _owner)
    {
        owner = _owner;
    }
    
}
