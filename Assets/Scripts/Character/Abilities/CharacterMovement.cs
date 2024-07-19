using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class CharacterMovement : CharacterAbility
{
    public float movementSpeed = 10f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    private CharacterController _characterController;
    private NavMeshAgent _aiController;
    private float _defaultMovementSpeed;
    private PlayerInputHandler _inputHandler;
    private CharacterOrientation _orientation;

    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        _orientation = GetComponent<CharacterOrientation>();
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

    public override void Tick()
    {
        if (characterCenter.isPlayerControlled)
        {
            Vector2 input = _inputHandler.GetMovementInput();
            moveDirection = new Vector3(input.x, 0, input.y).normalized;

            if (moveDirection != Vector3.zero)
            {
                _characterController.Move(moveDirection * (movementSpeed * Time.deltaTime));
                if (characterCenter.movementState != CharacterMovementState.Running)
                {
                    characterCenter.SetMovementState(CharacterMovementState.Running);
                    characterCenter.animator.SetBool("isRunning", true);
                }
            }
            else if (characterCenter.movementState == CharacterMovementState.Running)
            {
                characterCenter.SetMovementState(CharacterMovementState.Idle);
                characterCenter.animator.SetBool("isRunning", false);
            }
        }
        else
        {
            moveDirection = _aiController.velocity.normalized;

            if (moveDirection != Vector3.zero && characterCenter.movementState != CharacterMovementState.Running)
            {
                characterCenter.SetMovementState(CharacterMovementState.Running);
                characterCenter.animator.SetBool("isRunning", true);
            }
            else if (moveDirection == Vector3.zero && characterCenter.movementState == CharacterMovementState.Running)
            {
                characterCenter.SetMovementState(CharacterMovementState.Idle);
                characterCenter.animator.SetBool("isRunning", false);
            }
        }

        Vector3 animDirection;
        if (_orientation.lookDirection == Vector3.zero)
        {
            animDirection = Vector3.zero;
        }
        else
        {
            animDirection = Quaternion.Inverse(Quaternion.LookRotation(_orientation.lookDirection)) * moveDirection;
        }
        characterCenter.animator.SetFloat("moveX", animDirection.x);
        characterCenter.animator.SetFloat("moveY", animDirection.z);

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
