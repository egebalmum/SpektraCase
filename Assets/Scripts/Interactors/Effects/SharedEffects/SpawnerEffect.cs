using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEffect : InteractorEffect
{
    public Interactor nextPhase;

    public override void DestroyEffect()
    {
        Interactor _interactor = Instantiate(nextPhase, Interactor.transform.position, Quaternion.identity);
        _interactor.InitializeInteractor(Interactor.owner);
        _interactor.ResetInteractor();
        _interactor.PrepareInteractor(Interactor.transform);
        _interactor.CastInteractor();
    }
}
