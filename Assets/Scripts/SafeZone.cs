using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Logic for the safe zone in the game
// safe zones were not implemented due to time constraints leaving no room for generating them in the maze
public class SafeZone : MonoBehaviour
{
    // Materials based on if all items are collected or not, amrking if the workbench can be used
    [SerializeField] Material inactiveMaterial;
    [SerializeField] Material activeMaterial;

    // The floor of the safe zone to change color
    [SerializeField] GameObject safeZoneGround;

    //furniture to be built
    [SerializeField] GameObject furniturePrefab;
    // Spawn point for the furniture after building
    [SerializeField] GameObject furnitureSpawnPoint;

    // Flag to check if the workbench can be used
    private bool _isActive = false;


   // sets _isActive and material of safeZone dependent on StateManager.allItemsCollected
    void Update()
    {
        _isActive = StateManager.allItemsCollected;

        if (_isActive)
        {
            safeZoneGround.GetComponent<Renderer>().material = activeMaterial;
        }
        else
        {
            safeZoneGround.GetComponent<Renderer>().material = inactiveMaterial;
        }
    }

    // Method to build furniture if all items are collected
    public void BuildFurniture()
    {
        if (StateManager.allItemsCollected)
        {
            StateManager.itemBuild = true; // Set the item build state to true
            GameObject furniture = Instantiate(furniturePrefab, furnitureSpawnPoint.transform.position, Quaternion.identity); // Put the build furniture at the spawn point
            furniture.transform.Rotate(-90, 0, 0);
            furniture.transform.SetParent(furnitureSpawnPoint.transform);
        }
    }
}
