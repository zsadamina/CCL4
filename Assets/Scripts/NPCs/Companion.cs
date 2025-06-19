using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    public float stoppingDistanceThreshold = 0.5f; // Adjust based on agent's stopping distance
    
    private NavMeshAgent _navMeshAgent;
    
    [SerializeField]
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(target.position);

        if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance + stoppingDistanceThreshold)
        {
            // Agent is moving towards a destination
        }
        else
        {
            // Agent is standing still or has reached its destination
        }
    }
}
