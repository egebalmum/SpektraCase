using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class AIController : CharacterAbility
{
    [HideInInspector] public AIBehaviour currentBehaviour;
    public string targetName;
    [HideInInspector] public Transform player;
    public float chaseDistance = 10f;
    public float shootDistance = 5f;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public AIBehaviour patrollingBehaviour;
    [HideInInspector] public AIBehaviour chasingBehaviour;
    [HideInInspector] public AIBehaviour shootingBehaviour;

    void Start()
    {
        player = FindObjectsOfType<CharacterCenter>().First(character => character.name.Equals(targetName)).transform;
        agent = GetComponent<NavMeshAgent>();
        
        patrollingBehaviour = GetComponent<PatrollingBehaviour>();
        chasingBehaviour = GetComponent<ChasingBehaviour>();
        shootingBehaviour = GetComponent<ShootingBehaviour>();

        patrollingBehaviour.Initialize(this);
        chasingBehaviour.Initialize(this);
        shootingBehaviour.Initialize(this);

        SetState(patrollingBehaviour);
    }

    public override void Tick()
    {
        currentBehaviour.Tick();
    }

    public void SetState(AIBehaviour newBehaviour)
    {
        if (currentBehaviour != null)
        {
            currentBehaviour.OnExit();
        }

        currentBehaviour = newBehaviour;

        if (currentBehaviour != null)
        {
            currentBehaviour.OnEnter();
        }
    }

    public bool HasLineOfSight()
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
