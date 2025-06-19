using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public List<PickupItemClass> Inventory { get; set; } = new List<PickupItemClass>();

    private InventoryManager _inventoryManager;

    private ClipboardManager _clipboardManager;

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

        _inventoryManager = InventoryManager.Instance;
        _clipboardManager = ClipboardManager.Instance;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddItem(PickupItemClass item)
    {
        Inventory.Add(item);
        UpdateUI();

    }

    public void RemoveItem(PickupItemClass item)
    {
        Inventory.Remove(item);
        UpdateUI();
    }

    void UpdateUI()
    {
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
