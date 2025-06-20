using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

// class to represent a pickup item
public class PickupItemClass
{
    public string Name { get; set; } // Name of the item
    public GameObject prefab { get; set; } // Prefab of the item
    public Sprite sprite { get; set; } // Sprite of the item for UI representation

    // Constructor to initialize the pickup item
    public PickupItemClass(string name, GameObject prefab, Sprite sprite)
    {
        this.Name = name;
        this.prefab = prefab;
        this.sprite = sprite;
    }
}

// holds the pickupitems in a todo object
public class TodoItem : MonoBehaviour
{
    // variable to hold the pickup item game object
    private GameObject _pickupItemGameObject;

    // variable to hold the pickup item class in the todo item
    private PickupItemClass _pickupItem;
    // variable to hold the spawn point manager
    private SpawnPointManager _spawnPointManager;

    void Awake()
    {
        // get the spawn point manager instance
        _spawnPointManager = SpawnPointManager.Instance;
        // add the todos 
        _spawnPointManager.Todos.Add(this);
    }

    // method to initialize the pickup item
    public void InitPickupItem(PickupItemClass prefab)
    {

        _pickupItemGameObject = Instantiate(prefab.prefab, this.GetSpawnPoint(), Quaternion.identity);
        _pickupItemGameObject.GetComponent<CollectableItem>()._pickupItem = prefab;

        _pickupItemGameObject.transform.SetParent(this.gameObject.transform);
        Debug.Log("Picked up itemName: " + this.gameObject.name);
    }

    // method to get the pickup item class
    public Vector3 GetSpawnPoint()
    {
        return this.gameObject.transform.position;
    }
}
