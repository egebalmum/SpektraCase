using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponUpgrade : ScriptableObject
{
    public Sprite image;
    public abstract void AddUpgrade(Weapon weapon);

    public abstract void RemoveUpgrade(Weapon weapon);
}
