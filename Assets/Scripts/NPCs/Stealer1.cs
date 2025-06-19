using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Stealer : MonoBehaviour
{
    [SerializeField] public float stoppingDistanceThreshold = 0.5f; // Adjust based on agent's stopping distance

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private StateManager _stateManager;

    [SerializeField] private float stealIntervall = 2000f;
    [SerializeField] private float stealRange = 1f;
    public bool YoinkMode { get; set; } = true;

    [SerializeField] private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _stateManager = StateManager.Instance;
        _navMeshAgent.SetDestination(target.position);

    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (YoinkMode)
        {
            _navMeshAgent.SetDestination(target.position);
            StealItem();
        }
        else
        {
             Debug.Log("Distance: " +_navMeshAgent.remainingDistance + " YoinkMode: " + YoinkMode);

            if (_navMeshAgent.remainingDistance < 0.1)
            {
                this.gameObject.SetActive(false);
                Debug.Log("I am Gone");
            }
        }
    }

    void StealItem()
    {
        Debug.Log("Distance: " +_navMeshAgent.remainingDistance + " YoinkMode: " + YoinkMode);
        if (_navMeshAgent.remainingDistance < stealRange && _navMeshAgent.remainingDistance != 0)
        {
            Debug.Log("Yoink");
            var inventory = _stateManager.Inventory;
            _navMeshAgent.SetDestination(new Vector3(0, 0, 0));
            YoinkMode = false;

            if (inventory.Count == 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, inventory.Count - 1);
            var item = inventory[randomIndex];
            inventory.RemoveAt(randomIndex);
            Debug.Log("Stolen Item:"+ item.Name);
            TodoItem spawnpoint = InventoryManager.Instance.getRandomSpawnPointRange(1).FirstOrDefault();
            if (spawnpoint)
            {
                spawnpoint.InitPickupItem(item);
            }
        }
    }
}