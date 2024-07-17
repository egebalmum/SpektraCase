using UnityEngine;
using UnityEngine.AI;

public class CharacterPushability : CharacterAbility
{
    public float pushForceDecay = 5.0f;
    private CharacterController _characterController;
    private NavMeshAgent _aiController;
    private Vector3 pushForce;

    public override void Initialize()
    {
        _characterController = GetComponent<CharacterController>();
        _aiController = GetComponent<NavMeshAgent>();
    }
    
    public override void Tick()
    {
        if (pushForce.magnitude > 0.1f)
        {
            if (_characterController != null)
            {
                _characterController.Move(pushForce * Time.deltaTime);
            }
            else if (_aiController != null)
            {
                _aiController.Move(pushForce* Time.deltaTime);
            }
            
            pushForce = Vector3.Lerp(pushForce, Vector3.zero, pushForceDecay * Time.deltaTime);
        }
    }

    public void ApplyPush(Vector3 direction, float force)
    {
        if (!GetAbilityEnabled())
        {
            return;
        }
        pushForce = direction.normalized * force;
    }


    public override void ResetAbility()
    {
        pushForce = Vector3.zero;
    }

    public override void OnDeath()
    {
        ResetAbility();
    }
}