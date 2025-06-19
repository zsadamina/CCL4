using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

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

    
    private GameObject _pickupItemGameObject;
    private PickupItemClass _pickupItem;
    private RoomGenerator _roomGenerator;

    void Awake()
    {
        _roomGenerator = RoomGenerator.Instance;
        _roomGenerator.Todos.Add(this);
    }
    
    public void InitPickupItem(PickupItemClass prefab)
    {
        
        _pickupItemGameObject = Instantiate(prefab.prefab, this.GetSpawnPoint(), Quaternion.identity);
        _pickupItemGameObject.GetComponent<CollectableItem>()._pickupItem = prefab;
        
        _pickupItemGameObject.transform.SetParent(this.gameObject.transform);
        Debug.Log("Picked up itemName: " +this.gameObject.name);
    }

    public Vector3 GetSpawnPoint()
    {
        return this.gameObject.transform.position;
    }
}
