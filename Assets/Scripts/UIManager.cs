using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ClipboardManager _clipboardManager;
    private InventoryManager _inventoryManager;
    private StateManager _stateManager;

    public static UIManager Instance;

    // boolean to determine if the current scene is a tutorial scene
    [SerializeField]
    public bool Tutorial;

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

        _clipboardManager = ClipboardManager.Instance;
        _inventoryManager = InventoryManager.Instance;
        _stateManager = StateManager.Instance;
    }

    //Create all the items in the inventory UI on first load
    void Start()
    {
        UpdateUI();
    }

    // Add or remove items from the inventory and update the UI
    public void AddItem(PickupItemClass item)
    {
        _stateManager.Inventory.Add(item);
        UpdateUI();

    }

    public void RemoveItem(PickupItemClass item)
    {
        _stateManager.Inventory.Remove(item);
        UpdateUI();
    }

    // Update the UI to reflect the current state of the inventory
    void UpdateUI()
    {
        // Clear the clipboard item list container
        var Inventory = _stateManager.Inventory;
        foreach (Transform child in _clipboardManager.itemListContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // create dictionaries of the inventory and the items to be picked up to the scene
        // gorups them by name so we cann see how many of each item have to be picked up and have been picked up
        Dictionary<string, List<PickupItemClass>> pickUpItems = _inventoryManager.PickupItems
            .GroupBy(item => item.Name)
            .ToDictionary(group => group.Key, group => group.ToList());

        Dictionary<string, List<PickupItemClass>> inventoryList = Inventory
            .GroupBy(item => item.Name)
            .ToDictionary(group => group.Key, group => group.ToList());


        // create a clipboard item for each item to be picked up
        foreach (var item in pickUpItems)
        {
            // check how many items of this type are in the inventory
            var name = item.Key;
            var inventoryIntem = inventoryList.ContainsKey(name) ? inventoryList[name].Count : 0;
            // check if all items from the group have been picked up
            bool allItemsFromGroupAreInInventory = inventoryIntem == item.Value.Count;
            // create a clipboard item for the item
            _clipboardManager.setupClipboard(item.Value.FirstOrDefault(), pickUpItems[name].Count, inventoryIntem, allItemsFromGroupAreInInventory);
        }
    }

}
