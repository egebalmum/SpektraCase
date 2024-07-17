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
        Interactor _interactor = Instantiate(interactor, Interactor.transform.position, Quaternion.identity);
        _interactor.gameObject.SetActive(true);
        _interactor.InitializeInteractor(Interactor.owner);
        _interactor.ResetInteractor();
        _interactor.PrepareInteractor(Interactor.transform);
        _interactor.CastInteractor();
    }
}
