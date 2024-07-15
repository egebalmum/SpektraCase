using UnityEngine;

public class CharacterPushability : CharacterAbility
{
    public float pushForceDecay = 5.0f;
    private CharacterController _characterController;
    private Vector3 pushForce;

    public override void Initialize()
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    public override void EarlyTick()
    {
       
    }
    
    public override void Tick()
    {
        if (pushForce.magnitude > 0.1f)
        {
            _characterController.Move(pushForce * Time.deltaTime);
            pushForce = Vector3.Lerp(pushForce, Vector3.zero, pushForceDecay * Time.deltaTime);
        }
    }
    public override void LateTick()
    {
        
    }

    public void ApplyPush(Vector3 direction, float force)
    {
        pushForce = direction.normalized * force;
    }
}