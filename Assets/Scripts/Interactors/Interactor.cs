using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public virtual void DestroyInteractor() { }
    public virtual void ResetInteractor() { }
    public virtual void InitializeInteractor() { }
    public virtual void CastInteractor() { }

    public virtual void PrepareInteractor(Transform startingPoint) {}

}
