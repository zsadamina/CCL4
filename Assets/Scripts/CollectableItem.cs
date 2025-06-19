using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private StateManager _stateManager;

    public PickupItemClass _pickupItem { get; set; }

    void Awake()
    {
        _stateManager = StateManager.Instance;
    }
    
    // Start is called before the first frame update
    public void PickupItem()
    {
        Debug.Log("Picked up Item");
        Destroy(this.transform.parent.gameObject);
        
        if(_pickupItem != null){
            _stateManager.Inventory.Add(_pickupItem);
            Debug.Log(_stateManager.Inventory.Count);
        }
    }
}
