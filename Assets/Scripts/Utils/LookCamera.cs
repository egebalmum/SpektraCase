using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            Vector3 direction = mainCamera.transform.position - transform.position;
            direction.y = 0; // Keep the y-axis constant if you don't want the object to tilt
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}

