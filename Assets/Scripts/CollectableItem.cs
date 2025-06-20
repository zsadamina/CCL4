using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script for handling the collection of items in the game
public class CollectableItem : MonoBehaviour
{
    // reference to the StateManager, InventoryManager, and UIManager
    private StateManager _stateManager;
    private InventoryManager _inventoryManager;
    private UIManager _uiManager;

    // The item that will be picked up
    public PickupItemClass _pickupItem { get; set; }
    //Animator for the companion character that cheers when an item is picked up
    private Animator companionAnimator;

    [Header("Audio")]
    // Reference to the sound that plays when an item is picked up
    public AK.Wwise.Event pickupSound;

    // Gets references to the UIManager, InventoryManager, and StateManager on Awake
    void Awake()
    {
        _uiManager = UIManager.Instance;
        _inventoryManager = InventoryManager.Instance;
        _stateManager = StateManager.Instance;

        // Find the companion GameObject by tag and get its Animator component
        GameObject companion = GameObject.FindGameObjectWithTag("Companion");
        if (companion != null)
        {
            companionAnimator = companion.GetComponent<Animator>();
        }
    }

    // Method to handle the pickup of an item
    public void PickupItem()
    {
        // Destroy the parent GameObject of this script
        Destroy(this.transform.parent.gameObject);
        // Play the pickup sound
        pickupSound.Post(gameObject);

        if (_pickupItem != null)
        {
            // Add the item to the inventory and UI
            _uiManager.AddItem(_pickupItem);

            // Make the companion character cheer
            if (companionAnimator != null)
            {
                companionAnimator.SetTrigger("Cheer");
            }

            // If all the items are collected, update the state manager
            // If this is not the tutorial, load the end scene
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
        }
    }
}
