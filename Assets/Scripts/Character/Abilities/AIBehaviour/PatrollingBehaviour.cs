using UnityEngine;

public class PatrollingBehaviour : AIBehaviour
{
    public float patrolSpeed = 2f;
    [SerializeField] private Transform[] waypoints;
    [HideInInspector] public int currentWaypointIndex = 0;
    private CharacterOrientation _orientation;

    public override void Initialize(AIController controller)
    {
        base.Initialize(controller);
        if (_orientation == null)
        {
            _orientation = GetComponent<CharacterOrientation>();
        }
        foreach (var waypoint in waypoints)
        {
            waypoint.transform.parent = null;
        }
    }


    public override void OnEnter()
    {
        aiController.agent.speed = patrolSpeed;
        GoToNextWaypoint();
    }

    public override void Tick()
    {
        _orientation.AimAtPosition(aiController.agent.destination);
        if (!aiController.agent.pathPending && aiController.agent.remainingDistance < 0.5f)
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

        aiController.agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}