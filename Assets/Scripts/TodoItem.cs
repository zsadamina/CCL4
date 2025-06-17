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
    
    private GameObject _pickupItemGameObject;
    private PickupItemClass _pickupItem;
    
    void Awake()
    {
        _inventoryManager = InventoryManager.Instance;
        _inventoryManager.Todos.Add(this);
    }
    
    public void PickupItem()
    {
        Debug.Log("Picked up Item");
        Destroy(this.gameObject);
        Destroy(_pickupItemGameObject);
        
        if(_pickupItem != null){
            _inventoryManager.Inventory.Add(_pickupItem);
            Debug.Log(_inventoryManager.Inventory.Count);
        }
    }

    public void InitPickupItem(PickupItemClass prefab)
    {
        
        _pickupItemGameObject = Instantiate(prefab.prefab, this.GetSpawnPoint(), Quaternion.identity);
        _pickupItemGameObject.transform.SetParent(this.gameObject.transform);
        Debug.Log("Picked up itemName: " +this.gameObject.name);
    }

    public Vector3 GetSpawnPoint()
    {
        return this.gameObject.transform.position;
    }
}
