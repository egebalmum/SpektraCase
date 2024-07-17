using UnityEngine;

public class ChasingBehaviour : AIBehaviour
{
    public float chaseSpeed = 4f;
    private Vector3 lastKnownPosition;
    private bool isChasingLastKnownPosition = false;
    private CharacterOrientation _orientation;

    public override void OnEnter()
    {
        if (_orientation == null)
        {
            _orientation = GetComponent<CharacterOrientation>();
        }
        aiController.agent.speed = chaseSpeed;
        aiController.agent.isStopped = false;
        lastKnownPosition = aiController.player.position;
    }

    public override void Tick()
    {
        if (aiController.HasLineOfSight())
        {
            _orientation.AimAtPosition(aiController.player.position);
            lastKnownPosition = aiController.player.position;
            aiController.agent.SetDestination(lastKnownPosition);
            isChasingLastKnownPosition = false;
        }
        else if (!isChasingLastKnownPosition)
        {
            _orientation.AimAtPosition(lastKnownPosition);
            isChasingLastKnownPosition = true;
            aiController.agent.SetDestination(lastKnownPosition);
        }

        if (isChasingLastKnownPosition && aiController.agent.remainingDistance < 0.5f)
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
        isChasingLastKnownPosition = false;
    }
}