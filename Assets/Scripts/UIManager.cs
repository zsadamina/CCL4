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

    void Start()
    {
        UpdateUI();
    }
    
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
    
    // Start is called before the first frame update
    void UpdateUI()
    {
        var Inventory = _stateManager.Inventory;
        foreach (Transform child in _clipboardManager.transform)
        {
            Destroy(child.gameObject);
        }

        Dictionary<string, List<PickupItemClass>> pickUpItems = _inventoryManager.PickupItems
            .GroupBy(item => item.Name)
            .ToDictionary(group => group.Key, group => group.ToList());

        // Store the count of each item per group in inventoryList
        Dictionary<string, List<PickupItemClass>> inventoryList = Inventory
            .GroupBy(item => item.Name)
            .ToDictionary(group => group.Key, group => group.ToList());


        foreach (var item in pickUpItems)
        {
            var name = item.Key;
            var inventoryIntem = inventoryList.ContainsKey(name) ? inventoryList[name].Count : 0;
            bool allItemsFromGroupAreInInventory = inventoryIntem == item.Value.Count;

            _clipboardManager.setupClipboard(item.Value.FirstOrDefault(), pickUpItems[name].Count, inventoryIntem, allItemsFromGroupAreInInventory);
        }
    }
}
