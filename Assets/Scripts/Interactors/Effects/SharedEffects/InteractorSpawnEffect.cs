using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractorSpawnEffect : InteractorEffect
{
    public Interactor interactor;

    public override void DestroyEffect()
    {
        Interactor newInteractor = Instantiate(interactor, Interactor.transform.position, Quaternion.identity);
        newInteractor.gameObject.SetActive(true);
        newInteractor.InitializeInteractor(Interactor.owner);
        newInteractor.ResetInteractor();
        newInteractor.PrepareInteractor(Interactor.transform);
        newInteractor.CastInteractor();
    }
}
