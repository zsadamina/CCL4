using UnityEngine;
using UnityEngine.AI;
using AK.Wwise;

[RequireComponent(typeof(AkGameObj))]
public class CompanionFootstepsAudio : MonoBehaviour
{
    [Header("Footstep Settings")]
    [SerializeField] private AK.Wwise.Event footstepEvent;
    [SerializeField] private float stepInterval = 0.5f;  
    [SerializeField] private float minSpeed = 0.05f;     

    private NavMeshAgent agent;
    private float lastStepTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        lastStepTime = Time.time;
    }

    void Update()
    {
        bool isWalking = agent.velocity.magnitude > minSpeed && agent.remainingDistance > agent.stoppingDistance;

        if (isWalking && Time.time - lastStepTime >= stepInterval)
        {
            footstepEvent.Post(gameObject);  
            lastStepTime = Time.time;
        }
    }
}
