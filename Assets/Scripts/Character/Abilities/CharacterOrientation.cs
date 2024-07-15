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
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    public override void EarlyTick()
    {
        return;
    }

    public override void Tick()
    {
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = targetRotation;
        if (!GetAbilityEnabled())
        {
            return;
        }
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
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0;
            lookDirection = direction;
        }
    }
}