using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public static bool allItemsCollected = false;
    public List<PickupItemClass> Inventory {get; set;} = new List<PickupItemClass>();
    
    public static bool itemBuild = false;
    
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

        _clipboardManager = ClipboardManager.Instance;
        
        DontDestroyOnLoad(this);
    }
    
}
