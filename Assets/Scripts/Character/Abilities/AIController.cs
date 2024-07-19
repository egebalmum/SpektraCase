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
    [HideInInspector] public PatrollingBehaviour patrollingBehaviour;
    [HideInInspector] public ChasingBehaviour chasingBehaviour;
    [HideInInspector] public ShootingBehaviour shootingBehaviour;
    private NavMeshAgent _aiAgent;
    void Start()
    {
        _aiAgent = GetComponent<NavMeshAgent>();
        player = FindObjectsOfType<CharacterCenter>().First(character => character.name.Equals(targetName)).transform;
        InitializeBehaviour(ref patrollingBehaviour);
        InitializeBehaviour(ref chasingBehaviour);
        InitializeBehaviour(ref shootingBehaviour);

        SetState(patrollingBehaviour);
    }

    private void InitializeBehaviour<T>(ref T behaviour) where T : AIBehaviour
    {
        behaviour = GetComponent<T>();
        if (behaviour != null)
        {
            behaviour.Initialize(this);
        }
    }

    public override void Tick()
    {
        if (characterCenter.effectState == CharacterEffectState.Stunned)
        {
            _aiAgent.isStopped = true;
            return;
        }

        if (_aiAgent.isStopped)
        {
            _aiAgent.isStopped = false;
            return;
        }
        
        currentBehaviour?.Tick();
    }

    public void SetState(AIBehaviour newBehaviour)
    {
        currentBehaviour?.OnExit();
        currentBehaviour = newBehaviour;
        currentBehaviour?.OnEnter();
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
        SetState(null);
    }

    public override void OnRespawn()
    {
        SetState(patrollingBehaviour);
    }
}
