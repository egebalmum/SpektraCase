using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterMovement : CharacterAbility
{
    public float movementSpeed = 10f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    private CharacterController _characterController;
    private float _defaultMovementSpeed;
    private MovementIndicator _indicator;

    public override void Initialize()
    {
        _characterController = GetComponent<CharacterController>();
        _defaultMovementSpeed = movementSpeed;
        _indicator = GetComponentInChildren<MovementIndicator>();
    }


    public override void EarlyTick()
    {
        return;
    }

    public override void Tick()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
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