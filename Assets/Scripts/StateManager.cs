using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;
using UnityEngine.SceneManagement;

// Keeps track of thhe Inventory, state of collection and bulding, player health
public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public static bool allItemsCollected = false;
    public List<PickupItemClass> Inventory { get; set; } = new List<PickupItemClass>();

    public static bool itemBuild = false;

    private InventoryManager _inventoryManager;

    private ClipboardManager _clipboardManager;

    public static int playerHealth = 5;

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

    // reduce health and check if player is dead
    public void ReducePlayerHealth()
    {
        playerHealth--;
        if (playerHealth <= 0)
        {
            playerHealth = 5;
            allItemsCollected = false;
            Inventory.Clear();
            SceneManager.LoadScene("MainMenu");
        }
    }
    
}
