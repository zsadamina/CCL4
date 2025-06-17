using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupItemClass
{
    public string Name { get; set; }
    public GameObject prefab { get; set; }

    public PickupItemClass(string name, GameObject prefab)
    {
        this.Name = name;
        this.prefab = prefab;
    }
}

public class TodoItem : MonoBehaviour
{

    private InventoryManager _inventoryManager;
    
    void Awake()
    {
        _inventoryManager = InventoryManager.Instance;
    }
    
    public void PickupItem()
    {
        Debug.Log("Picked up Item");
        Destroy(this.gameObject);
        var item = _inventoryManager.PickupItems.FirstOrDefault();
        if(item != null){
            _inventoryManager.Inventory.Add(item);
            Debug.Log(_inventoryManager.Inventory.Count);
        }
    }
}
