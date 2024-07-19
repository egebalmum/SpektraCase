using UnityEngine;

public class ChasingBehaviour : AIBehaviour
{
    public float chaseSpeed = 4f;
    private Vector3 _lastKnownPosition;
    private bool _isChasingLastKnownPosition = false;
    private CharacterOrientation _orientation;
    private CharacterMovement _movement;

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        _orientation = GetComponent<CharacterOrientation>();
        _movement = GetComponent<CharacterMovement>();
    }

    public override void OnEnter()
    {
        _movement.GetAiController().ResetPath();
        _movement.AdjustMovementSpeed(chaseSpeed);
        _movement.GetAiController().isStopped = false;
        _lastKnownPosition = aiController.player.position;
    }

    public override void Tick()
    {
        if (aiController.HasLineOfSight())
        {
            _orientation.AimAtPosition(aiController.player.position);
            _lastKnownPosition = aiController.player.position;
            _movement.GetAiController().SetDestination(_lastKnownPosition);
            _isChasingLastKnownPosition = false;
        }
        else if (!_isChasingLastKnownPosition)
        {
            _orientation.AimAtPosition(_lastKnownPosition);
            _isChasingLastKnownPosition = true;
            _movement.GetAiController().SetDestination(_lastKnownPosition);
        }

        if (_isChasingLastKnownPosition && _movement.GetAiController().remainingDistance < 0.5f)
        {
            aiController.SetState(aiController.patrollingBehaviour);
        }

        if (Vector3.Distance(aiController.player.position, transform.position) <= aiController.shootDistance && aiController.HasLineOfSight())
        {
            aiController.SetState(aiController.shootingBehaviour);
        }
    }

    public override void OnExit()
    {
        _isChasingLastKnownPosition = false;
    }
}