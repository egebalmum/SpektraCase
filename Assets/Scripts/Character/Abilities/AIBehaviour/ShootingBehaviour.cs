using UnityEngine;

public class ShootingBehaviour : AIBehaviour
{
    private Weapon _weapon;
    private CharacterOrientation _orientation;
    private CharacterMovement _movement;

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        _weapon = (Weapon)GetComponent<CharacterHandleHand>().GetHandItem();
        _orientation = GetComponent<CharacterOrientation>();
        _movement = GetComponent<CharacterMovement>();
    }

    public override void OnEnter()
    {
        _movement.GetAiController().ResetPath();
        if (_weapon == null)
        {
            _weapon = (Weapon)GetComponent<CharacterHandleHand>().GetHandItem();
        }
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