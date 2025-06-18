using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{

    public PickupItemClass _pickupItem { get; set; }
    
    private InventoryManager _inventoryManager;

    void Awake()
    {
        _inventoryManager = InventoryManager.Instance;
    }
    
    // Start is called before the first frame update
    public void PickupItem()
    {
        Debug.Log("Picked up Item");
        Destroy(this.transform.parent.gameObject);
        
        if(_pickupItem != null){
            _inventoryManager.Inventory.Add(_pickupItem);
            Debug.Log(_inventoryManager.Inventory.Count);
        }
    }
}
