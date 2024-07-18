using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : CharacterAbility
{
    public float movementSpeed = 10f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    private CharacterController _characterController;
    private NavMeshAgent _aiController;
    private float _defaultMovementSpeed;
    private PlayerInputHandler _inputHandler;

    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        if (characterCenter.isPlayerControlled)
        {
            _characterController = GetComponent<CharacterController>();
        }
        else
        {
            _aiController = GetComponent<NavMeshAgent>();
        }
        _defaultMovementSpeed = movementSpeed;

        if (characterCenter.isPlayerControlled)
        {
            _inputHandler = new PlayerInputHandler();
        }
    }

    public override void EarlyTick() { }

    public override void Tick()
    {
        if (characterCenter.isPlayerControlled)
        {
            Vector2 input = _inputHandler.GetMovementInput();
            moveDirection = new Vector3(input.x, 0, input.y).normalized;
        }
        
        if (moveDirection != Vector3.zero) 
        { 
            _characterController.Move(moveDirection * (movementSpeed * Time.deltaTime));
            if (characterCenter.movementState != CharacterMovementState.Running)
            {
                characterCenter.SetMovementState(CharacterMovementState.Running);
            }
        }
        else
        {
            if (characterCenter.movementState == CharacterMovementState.Running)
            {
                characterCenter.SetMovementState(CharacterMovementState.Idle);
            }
        }
    }

    public CharacterController GetCharacterController()
    {
        return _characterController;
    }

    public NavMeshAgent GetAiController()
    {
        return _aiController;
    }
    public override void OnDeath()
    {
        if (characterCenter.isPlayerControlled)
        {
            _characterController.enabled = false;
        }
        else
        {
            _aiController.enabled = false;
        }
    }

    public override void OnRespawn()
    {
        if (characterCenter.isPlayerControlled)
        {
            _characterController.enabled = true;
        }
        else
        {
            _aiController.enabled = true;
            _aiController.ResetPath();
        }
    }

    public void AdjustMovementSpeed(float newSpeed)
    {
        if (characterCenter.isPlayerControlled)
        {
            movementSpeed = newSpeed;
        }
        else
        {
            _aiController.speed = newSpeed;
        }
    }
    
    public void ResetMovementSpeed()
    {
        if (characterCenter.isPlayerControlled)
        {
            movementSpeed = _defaultMovementSpeed;
        }
        else
        {
            _aiController.speed = _defaultMovementSpeed;
        }
    }
}
public partial class PlayerInputHandler
{
    public Vector2 GetMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontal, vertical);
    }
}
