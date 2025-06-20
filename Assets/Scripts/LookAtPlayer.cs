using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform playerCamera;

    void Start()
    {
        // Find the XR camera (usually tagged as "MainCamera")
        Camera cam = Camera.main;
        if (cam != null)
        {
            playerCamera = cam.transform;
        }
    }

    void Update()
    {
        if (playerCamera != null)
        {
            Vector3 lookDirection = playerCamera.position - transform.position;
            lookDirection.y = 0; // Keep rotation on the horizontal plane
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            }
        }
    }
}

