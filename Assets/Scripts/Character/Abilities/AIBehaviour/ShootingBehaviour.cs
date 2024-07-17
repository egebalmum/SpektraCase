using UnityEngine;

public class ShootingBehaviour : AIBehaviour
{
    private Weapon _weapon;
    private CharacterOrientation _orientation;
    public override void OnEnter()
    {
        if (_weapon == null)
        {
            _weapon = (Weapon)GetComponent<CharacterHandleHand>().GetHandItem();
        }

        if (_orientation == null)
        {
            _orientation = GetComponent<CharacterOrientation>();
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