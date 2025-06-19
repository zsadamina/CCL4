using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using AK.Wwise;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepsAudio : MonoBehaviour
{
    [Header("Movement")]
    public DynamicMoveProvider moveProvider;
    public bool useLeftHand = true;

    [Header("Wwise")]
    public AK.Wwise.Event footstepEvent; 
    public float stepInterval = 0.5f;

    private float stepTimer = 0f;

    void Update()
    {
        if (moveProvider == null || footstepEvent == null) return;

        Vector2 moveInput = useLeftHand
            ? moveProvider.leftHandMoveAction.action.ReadValue<Vector2>()
            : moveProvider.rightHandMoveAction.action.ReadValue<Vector2>();

        bool isMoving = moveInput.magnitude > 0.1f;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        footstepEvent.Post(gameObject);
    }
}
