using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    protected AIController aiController;

    public virtual void Initialize(AIController controller)
    {
        aiController = controller;
    }

    public abstract void Tick();
    public abstract void OnEnter();
    public abstract void OnExit();
}