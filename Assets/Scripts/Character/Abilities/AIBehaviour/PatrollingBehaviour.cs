using UnityEngine;

public class PatrollingBehaviour : AIBehaviour
{
    public float patrolSpeed = 2f;
    [SerializeField] private Transform[] waypoints;
    [HideInInspector] public int currentWaypointIndex = 0;
    private CharacterOrientation _orientation;
    private CharacterMovement _movement;

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        _orientation = GetComponent<CharacterOrientation>();
        _movement = GetComponent<CharacterMovement>();
        foreach (var waypoint in waypoints)
        {
            waypoint.transform.parent = null;
        }
    }


    public override void OnEnter()
    {
        _movement.AdjustMovementSpeed(patrolSpeed);
        GoToNextWaypoint();
    }

    public override void Tick()
    {
        _orientation.AimAtPosition(_movement.GetAiController().destination);
        if (!_movement.GetAiController().pathPending && _movement.GetAiController().remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }

        if (Vector3.Distance(aiController.player.position, transform.position) <= aiController.chaseDistance && aiController.HasLineOfSight())
        {
            aiController.SetState(aiController.chasingBehaviour);
        }
    }

    public override void OnExit() { }

    private void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        _movement.GetAiController().destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}