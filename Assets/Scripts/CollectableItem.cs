using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private StateManager _stateManager;

    public PickupItemClass _pickupItem { get; set; }
    
    private InventoryManager _inventoryManager;

    [Header("Audio")]
    public AK.Wwise.Event pickupSound;

    void Awake()
    {
        _stateManager = StateManager.Instance;
        _inventoryManager = InventoryManager.Instance;
    }
    
    // Start is called before the first frame update
    public void PickupItem()
    {
        Debug.Log("Picked up Item");
        Destroy(this.transform.parent.gameObject);
        pickupSound.Post(gameObject);
        
        if(_pickupItem != null){
            _stateManager.AddItem(_pickupItem);
            _stateManager.Inventory.Add(_pickupItem);
             if (_stateManager.Inventory.Count >= _inventoryManager.PickupItems.Length)
            {
                StateManager.allItemsCollected = true;
            }
            else
            {
                StateManager.allItemsCollected = false;
            }
            Debug.Log(_stateManager.Inventory.Count);
        }
    }
}
