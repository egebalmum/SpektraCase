using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : CharacterAbility
{
    [HideInInspector]
    public Vector3 lookDirection;
    private Camera _mainCamera;
    private bool _isPlayerControlled;

    public override void Initialize()
    {
        _isPlayerControlled = GetComponent<CharacterCenter>().characterName.Equals(LevelManager.instance.mainPlayerName);
        _mainCamera = Camera.main;
    }
    

    public override void Tick()
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = targetRotation;
        LookAtCursor();
    }

    private void LookAtCursor()
    {
        HandleInput();
    }

    public override void HandleInput()
    {
        if (!_isPlayerControlled)
        {
            return;
        }
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPosition);
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