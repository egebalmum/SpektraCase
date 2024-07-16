using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public CharacterCenter owner;
    
    public virtual void DestroyInteractor() { }
    public virtual void ResetInteractor() { }
    public virtual void InitializeInteractor(CharacterCenter owner) { }
    public virtual void CastInteractor() { }

    public virtual void PrepareInteractor(Transform startingPoint) {}

}
