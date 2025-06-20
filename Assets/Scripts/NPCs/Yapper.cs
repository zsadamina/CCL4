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


    private GameObject _locomotion;

    private int frameCounter = 0;
    private ClipboardManager _clipBoardManager;

    public bool YappingMode { get; set; } = true;
    private bool _yappingDone = false;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _stateManager = StateManager.Instance;
        target = GameObject.FindWithTag("Player").transform;
        _locomotion = GameObject.FindWithTag("Locomotion");
        _clipBoardManager = ClipboardManager.Instance;
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
        if (!_yappingDone)
            StartYapping();
        
        if (YappingMode)
        {
            _navMeshAgent.SetDestination(target.position);
        }
        else
        {
            _navMeshAgent.SetDestination(Vector3.zero);
            frameCounter++;

            if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance && frameCounter > 5)
            {
                Destroy(this.gameObject);
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
            _yappingDone = true;
        }
    }

    void StopYapping()
    {
        YappingMode = false;
        _clipBoardManager.ReduceHealth();
        _stateManager.ReducePlayerHealth();
        _locomotion.SetActive(true);
        _animator.SetBool("IsTalking", false);
        Debug.Log(_navMeshAgent.isActiveAndEnabled && _navMeshAgent.isOnNavMesh);
    }
}