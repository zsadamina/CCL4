using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;


[RequireComponent(typeof(SpawnPointManager))]
//Handles spawn of the items and List of items to be collected
public class InventoryManager : MonoBehaviour
{
    // Singleton instance of InventoryManager
    public static InventoryManager Instance;

    // Serialized fields for item prefabs and their associated sprites for the clipboard
    [SerializeField] private GameObject[] itemPrefabsArray;
    [SerializeField] private Sprite[] itemSpritesArray;

    // Array of PickupItemClass instances
    public PickupItemClass[] PickupItems;
    
    // Reference to the RoomGenerator and SpawnPointManager
    private RoomGenerator _roomGenerator;
    private SpawnPointManager _spawnpointManager;

    // Initialize the singleton instance and populate PickupItems
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
        
        _spawnpointManager = SpawnPointManager.Instance;

        // Sets the pickup items based on the prefabs and sprites provided
        // Creates instances of PickupItemClass for each item prefab and sprite
        PickupItems = itemPrefabsArray.Select((itemPrefab, i) =>
            new PickupItemClass(itemPrefab.name, itemPrefab, itemSpritesArray[i])
        ).ToArray();
    }

    void Start()
    {
        InitSpawnPoints();
    }

    // Initializes spawn points for the items
    private void InitSpawnPoints()
    {
        var selectedSpawnPoints = getRandomSpawnPointRange(PickupItems.Length);
        if (selectedSpawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available for item initialization.");
            return;
        }

        // Initializes each selected spawn point with a corresponding PickupItemClass
        Debug.Log(selectedSpawnPoints.Count);
        for (int i = 0; i < PickupItems.Length; i++)
        {
            selectedSpawnPoints[i].InitPickupItem(PickupItems[i]);
        }
    }

    // Returns a random range of spawn points from the SpawnPointManager
    public List<TodoItem> getRandomSpawnPointRange(int range)
    {
        List<TodoItem> todos = _spawnpointManager.Todos;
        if (todos.Count <= 0)
        {
            return new List<TodoItem>();
        }
        var shuffledList = todos.OrderBy(x => Random.value).Where(item => item.transform.childCount == 0).ToList();
        return shuffledList.GetRange(0, range);
    }
}