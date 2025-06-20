using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableItem : MonoBehaviour
{
    private StateManager _stateManager;

    public PickupItemClass _pickupItem { get; set; }
    
    private InventoryManager _inventoryManager;

    [Header("Audio")]
    public AK.Wwise.Event pickupSound;

    private UIManager _uiManager;

    void Awake()
    {
        _uiManager = UIManager.Instance;
        _inventoryManager = InventoryManager.Instance;
        _stateManager = StateManager.Instance;
    }
    
    // Start is called before the first frame update
    public void PickupItem()
    {
        Debug.Log("Picked up Item");
        Destroy(this.transform.parent.gameObject);
        pickupSound.Post(gameObject);
        
        if(_pickupItem != null){
            _uiManager.AddItem(_pickupItem);
             if (_stateManager.Inventory.Count >= _inventoryManager.PickupItems.Length)
            {
                StateManager.allItemsCollected = true;
                if (!_uiManager.Tutorial)
                {
                    SceneManager.LoadScene("EndScene");
                }
            }
            else
            {
                StateManager.allItemsCollected = false;
            }
            Debug.Log(_stateManager.Inventory.Count);
        }
    }
}
