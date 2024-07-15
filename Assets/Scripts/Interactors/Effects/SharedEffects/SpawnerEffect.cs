using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEffect : InteractorEffect
{
    [SerializeField] private Interactor nextPhase;

    public override void DestroyEffect()
    {
        Interactor _interactor = Instantiate(nextPhase, Interactor.transform.position, Quaternion.identity);
        _interactor.InitializeInteractor();
        _interactor.ResetInteractor();
        _interactor.PrepareInteractor(Interactor.transform);
        _interactor.CastInteractor();
    }
}
