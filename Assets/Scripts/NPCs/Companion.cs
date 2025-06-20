using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    [SerializeField]
    public float stoppingDistanceThreshold = 0.5f; // Adjust based on agent's stopping distance
    
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    
    [SerializeField]
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _navMeshAgent.SetDestination(target.position);
        if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance - _navMeshAgent.stoppingDistance > _navMeshAgent.stoppingDistance + stoppingDistanceThreshold)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}
