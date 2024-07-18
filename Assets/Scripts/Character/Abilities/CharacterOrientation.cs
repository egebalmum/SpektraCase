using UnityEngine;

public class CharacterOrientation : CharacterAbility
{
    [HideInInspector]
    public Vector3 lookDirection;
    private Camera _mainCamera;
    private bool _isPlayerControlled;
    private PlayerInputHandler _inputHandler;

    public override void Initialize(CharacterCenter characterCenter)
    {
        base.Initialize(characterCenter);
        _isPlayerControlled = GetComponent<CharacterCenter>().characterName.Equals(LevelManager.instance.mainPlayerName);
        _mainCamera = Camera.main;

        if (_isPlayerControlled)
        {
            _inputHandler = new PlayerInputHandler();
        }
    }

    public override void Tick()
    {
        if (_isPlayerControlled)
        {
            Vector3 mousePosition = _inputHandler.GetMousePosition();
            AimAtMousePosition(mousePosition);
        }

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = targetRotation;
    }

    private void AimAtMousePosition(Vector3 mousePosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        float planeY = transform.position.y;
        float distanceToPlane = (planeY - ray.origin.y) / ray.direction.y;

        if (distanceToPlane > 0)
        {
            Vector3 hitPoint = ray.origin + ray.direction * distanceToPlane;
            AimAtPosition(hitPoint);
        }
    }

    public void AimAtPosition(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0;
        lookDirection = direction;
    }
}
public partial class PlayerInputHandler
{
    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }
}

