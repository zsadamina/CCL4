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
}