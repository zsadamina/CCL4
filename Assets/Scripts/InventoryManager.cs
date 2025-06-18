using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    [SerializeField] private GameObject[] itemPrefabsArray;

    public PickupItemClass[] PickupItems;

    public List<PickupItemClass> Inventory {get; set;} = new List<PickupItemClass>();
    
    public List<TodoItem> Todos {get; set;} = new List<TodoItem>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        
        PickupItems = itemPrefabsArray.Select(itemPrefab =>
            new PickupItemClass(itemPrefab.name, itemPrefab)).ToArray();
    }

    void Start()
    {
        InitSpawnPoints();
    }

    private void InitSpawnPoints()
    {
        if (Todos.Count <= 0)
        {
            return;
        }
       var shuffledList = Todos.OrderBy( x => Random.value ).ToList();
       var selectedSpawnPoints = shuffledList.GetRange(0, Todos.Count);
       Debug.Log(selectedSpawnPoints.Count);
       for (int i = 0; i < PickupItems.Length; i++)
       {
           selectedSpawnPoints[i].InitPickupItem(PickupItems[i]);
       } 
    }
}