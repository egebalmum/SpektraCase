using UnityEngine;

public class CharacterMovement : CharacterAbility
{
    public float movementSpeed = 10f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    private CharacterController _characterController;
    private float _defaultMovementSpeed;
    private MovementIndicator _indicator;
    private bool _isPlayerControlled;
    private PlayerInputHandler _inputHandler;

    public override void Initialize()
    {
        _isPlayerControlled = GetComponent<CharacterCenter>().characterName.Equals(LevelManager.instance.mainPlayerName);
        _characterController = GetComponent<CharacterController>();
        _defaultMovementSpeed = movementSpeed;
        _indicator = GetComponentInChildren<MovementIndicator>();

        if (_isPlayerControlled)
        {
            _inputHandler = new PlayerInputHandler();
        }
    }

    public override void EarlyTick() { }

    public override void Tick()
    {
        if (_isPlayerControlled)
        {
            Vector2 input = _inputHandler.GetMovementInput();
            moveDirection = new Vector3(input.x, 0, input.y).normalized;
        }
        
        if (moveDirection != Vector3.zero) 
        { 
            _characterController.Move(moveDirection * (movementSpeed * Time.deltaTime));
        }
    }

    public override void LateTick()
    {
        if (moveDirection == Vector3.zero)
        {
            _indicator.transform.localScale = Vector3.zero;
        }
        else
        {
            _indicator.transform.localScale = Vector3.one;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _indicator.transform.rotation = targetRotation;
        }
    }

    public void AdjustMovementSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }
    
    public void ResetMovementSpeed()
    {
        movementSpeed = _defaultMovementSpeed;
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
