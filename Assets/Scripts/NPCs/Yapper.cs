using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Yapper : MonoBehaviour
{
    [SerializeField] private float stoppingDistanceThreshold = 0.5f; // Adjust based on agent's stopping distance

    [SerializeField] private float yappingDistance = 1f;

    [SerializeField] private float yappingTime = 2f;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private StateManager _stateManager;

    [SerializeField] private Transform target;


    [SerializeField] private GameObject _locomotion;

    private int frameCounter = 0;
    
    public bool YappingMode { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _stateManager = StateManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        MoveAgent();
        if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance - _navMeshAgent.stoppingDistance >
            _navMeshAgent.stoppingDistance + stoppingDistanceThreshold)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }

    void MoveAgent()
    {
        if (YappingMode)
        {
            _navMeshAgent.SetDestination(target.position);
            StartYapping();
        }
        else
        {
            _navMeshAgent.SetDestination(Vector3.zero);
            frameCounter++;
            Debug.Log("Distance: " + _navMeshAgent.remainingDistance + " YoinkMode: " + YappingMode);

            if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance && frameCounter > 3)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    void StartYapping()
    {
        if (_navMeshAgent.remainingDistance < yappingDistance && _navMeshAgent.remainingDistance != 0)
        {
            Debug.Log("YappYappYapp");
            _locomotion.SetActive(false);
            _animator.SetBool("IsTalking", true);
            Invoke("StopYapping", yappingTime);
        }
    }

    void StopYapping()
    {
        _locomotion.SetActive(true);
        YappingMode = false;
        _animator.SetBool("IsTalking", false);
        Debug.Log(_navMeshAgent.isActiveAndEnabled && _navMeshAgent.isOnNavMesh);
    }
}