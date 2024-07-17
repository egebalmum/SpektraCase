using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Patrolling,
    Chasing,
    Shooting
}

public class AIMovement : CharacterAbility
{
    public AIState currentState;
    public Transform[] waypoints;
    public string targetName;
    private Transform player;
    public float chaseDistance = 10f;
    public float shootDistance = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private SphereCollider _collider;

    void Start()
    {
        _collider = gameObject.AddComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = chaseDistance;
        player = FindObjectsOfType<CharacterCenter>().First(character => character.name.Equals(targetName)).transform;
        agent = GetComponent<NavMeshAgent>();
        currentState = AIState.Patrolling;
        agent.speed = patrolSpeed;
        foreach (var waypoint in waypoints)
        {
            waypoint.parent = null;
        }
        GoToNextWaypoint();
    }

    public override void Tick()
    {
        switch (currentState)
        {
            case AIState.Patrolling:
                Patrol();
                break;
            case AIState.Chasing:
                Chase();
                break;
            case AIState.Shooting:
                Shoot();
                break;
        }
        CheckDistanceToPlayer();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void Shoot()
    {
        // Implement shooting logic here
        Debug.Log("Shooting at the player!");
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= shootDistance && HasLineOfSight())
        {
            currentState = AIState.Shooting;
            agent.isStopped = true;
        }
        else if (distanceToPlayer <= chaseDistance && HasLineOfSight())
        {
            currentState = AIState.Chasing;
            agent.isStopped = false;
            agent.speed = chaseSpeed;
        }
        else
        {
            currentState = AIState.Patrolling;
            agent.isStopped = false;
            agent.speed = patrolSpeed;
        }
    }

    private bool HasLineOfSight()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, chaseDistance))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            currentState = AIState.Chasing;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            currentState = AIState.Patrolling;
        }
    }

    public override void OnDeath()
    {
        agent.enabled = false;
    }

    public override void OnRespawn()
    {
        agent.enabled = true;
        agent.ResetPath();
    }
}
