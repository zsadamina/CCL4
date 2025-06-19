using UnityEngine;
using UnityEngine.InputSystem;
using AK.Wwise;
using UnityEngine.AI;

[RequireComponent(typeof(AkGameObj))]
public class CompanionFootstepsAudio : MonoBehaviour
{
   [Header("Footstep Settings")]
    public AK.Wwise.Event footstepEvent;
    public float stepInterval = 0.5f;         // Seconds between steps while sliding
    public float minSpeed = 0.05f;            // Slide threshold

    private NavMeshAgent agent;
    private float lastStepTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastStepTime = Time.time;
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;

        if (speed > minSpeed && Time.time - lastStepTime >= stepInterval)
        {
            footstepEvent.Post(gameObject); // Plays from cube's position
            lastStepTime = Time.time;
        }
    }
}


