using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOrientation : CharacterAbility
{
    
    [HideInInspector]
    public Vector3 lookDirection;
    private Camera _mainCamera;
    public override void Initialize()
    {
        _mainCamera = Camera.main;
    }

    public override void EarlyTick()
    {
        return;
    }

    public override void Tick()
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = targetRotation;
        LookAtCursor();
    }

    public override void LateTick()
    {
        return;
    }

    private void LookAtCursor()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mouseScreenPosition);
        float planeY = transform.position.y;
        float distanceToPlane = (planeY - ray.origin.y) / ray.direction.y;
    
        if (distanceToPlane > 0)
        {
            Vector3 hitPoint = ray.origin + ray.direction * distanceToPlane;
            Vector3 direction = hitPoint - transform.position;
            direction.y = 0; // Ensure the direction is horizontal
            lookDirection = direction;
        }
    }

}