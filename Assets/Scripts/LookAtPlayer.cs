using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scripts to make an object (specifically yrsa in the tutorial) look at the player camera in XR
public class LookAtPlayer : MonoBehaviour
{
    private Transform playerCamera;

    void Start()
    {
        // Find the XR camera
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
            // Calculate the direction to look at the player camera
            Vector3 lookDirection = playerCamera.position - transform.position;
            lookDirection.y = 0; // Keep rotation on the horizontal plane
            if (lookDirection != Vector3.zero)
            {
                // Normalize the direction and apply rotation
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            }
        }
    }
}

