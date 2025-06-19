using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

[RequireComponent(typeof(SpawnPointManager))]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    
    [SerializeField] private GameObject[] itemPrefabsArray;
    [SerializeField] private Sprite[] itemSpritesArray;

    public PickupItemClass[] PickupItems;
    
    private RoomGenerator _roomGenerator;
    private SpawnPointManager _spawnpointManager;
    
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

        _spawnpointManager = SpawnPointManager.Instance;

        PickupItems = itemPrefabsArray.Select((itemPrefab, i) =>
            new PickupItemClass(itemPrefab.name, itemPrefab, itemSpritesArray[i])
        ).ToArray();
    }

    void Start()
    {
        InitSpawnPoints();
    }

    private void InitSpawnPoints()
    {
        var selectedSpawnPoints = getRandomSpawnPointRange(PickupItems.Length);
        if( selectedSpawnPoints.Count == 0 )
        {
            Debug.LogWarning("No spawn points available for item initialization.");
            return;
        }
        
       Debug.Log(selectedSpawnPoints.Count);
       for (int i = 0; i < PickupItems.Length; i++)
       {
           selectedSpawnPoints[i].InitPickupItem(PickupItems[i]);
       } 
    }

    public List<TodoItem> getRandomSpawnPointRange(int range)
    {
        List<TodoItem> todos = _spawnpointManager.Todos;
        if (todos.Count <= 0)
        {
            return new List<TodoItem>();
        }
        var shuffledList = todos.OrderBy( x => Random.value ).Where(item => item.transform.childCount == 0).ToList();
        return shuffledList.GetRange(0, range);
    }
}