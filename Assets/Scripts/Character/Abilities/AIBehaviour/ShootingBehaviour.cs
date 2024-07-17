using UnityEngine;

public class ShootingBehaviour : AIBehaviour
{
    private Weapon _weapon;
    private CharacterOrientation _orientation;

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        _weapon = (Weapon)GetComponent<CharacterHandleHand>().GetHandItem();
        _orientation = GetComponent<CharacterOrientation>();
    }

    public override void OnEnter()
    {
        if (_weapon == null)
        {
            _weapon = (Weapon)GetComponent<CharacterHandleHand>().GetHandItem();
        }
        aiController.agent.isStopped = true;
    }

    public override void Tick()
    {
        _weapon.TryShoot();
        _orientation.AimAtPosition(aiController.player.position);
        if (Vector3.Distance(aiController.player.position, transform.position) > aiController.shootDistance || !aiController.HasLineOfSight())
        {
            aiController.SetState(aiController.chasingBehaviour);
        }
    }

    public override void OnExit() { }
}